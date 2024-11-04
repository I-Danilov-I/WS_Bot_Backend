using OpenCvSharp;
using Tesseract;

namespace WhiteoutSurvival_Bot.TextReader
{
    public class TextRecogntion(DeviceControl.AdbCommandExecutor adb) : Settings.Configuration
    {

        public bool TakeScreenshot()
        {
            Directory.CreateDirectory(ScreenshotDirectory);
            Thread.Sleep(1000);
            adb.ExecuteAdbCommand("shell screencap -p /sdcard/screenshot.png");  // Screenshot auf Emulator
            if (CheckFileOnDevice("/sdcard/screenshot.png")) // Warte, bis der Screenshot erstellt wurde und verfügbar ist
            {
                adb.ExecuteAdbCommand($"pull /sdcard/screenshot.png {ScreenshotDirectory}"); // Screenshot auf PC
                if (CheckFileOnPC($"{ScreenshotDirectory}/screenshot.png"))  // Warte, bis das Bild erfolgreich auf den PC übertragen wurde
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private bool CheckFileOnDevice(string filePath)
        {
            string result = adb.ExecuteAdbCommand($"shell ls {filePath}");
            return !result.Contains("No such file");
        }


        private bool CheckFileOnPC(string filePath)
        {
            int attempts = 0;
            while (attempts < 200) // maximal 
            {
                if (File.Exists(filePath))
                {
                    return true;
                }
                //Thread.Sleep(10); // kurze Pause, um die Dateiübertragung zu ermöglichen
                attempts++;
            }
            return false;
        }


        internal string ProcessImageAndExtract(string imagePath, string trainedDataDirectory, string? color = null)
        {
            string language = "deu";

            // Bild laden
            Mat img = Cv2.ImRead(imagePath);

            // 1. Bildgröße anpassen, um die Erkennungsqualität zu verbessern
            Cv2.Resize(img, img, new Size(img.Width * 2, img.Height * 2));

            Mat processedImg;

            if (string.IsNullOrEmpty(color))
            {
                // Allgemeine Verarbeitung ohne Farbfokussierung
                processedImg = new Mat();
                Cv2.CvtColor(img, processedImg, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(processedImg, processedImg, 127, 255, ThresholdTypes.BinaryInv);
            }
            else
            {
                // Farbmaskenverarbeitung basierend auf dem Farbparameter
                Mat hsvImg = new Mat();
                Cv2.CvtColor(img, hsvImg, ColorConversionCodes.BGR2HSV);

                Mat colorMask = new Mat();
                switch (color.ToLower())
                {
                    case "weiß":
                        Scalar lowerWhite = new Scalar(0, 0, 200);
                        Scalar upperWhite = new Scalar(180, 30, 255);
                        Cv2.InRange(hsvImg, lowerWhite, upperWhite, colorMask);
                        break;

                    case "grün":
                        Scalar lowerGreen = new Scalar(35, 40, 40);
                        Scalar upperGreen = new Scalar(85, 255, 255);
                        Cv2.InRange(hsvImg, lowerGreen, upperGreen, colorMask);
                        break;

                    case "rot":
                        Scalar lowerRed1 = new Scalar(0, 50, 50);
                        Scalar upperRed1 = new Scalar(10, 255, 255);
                        Scalar lowerRed2 = new Scalar(170, 50, 50);
                        Scalar upperRed2 = new Scalar(180, 255, 255);
                        Mat redMask1 = new Mat();
                        Mat redMask2 = new Mat();
                        Cv2.InRange(hsvImg, lowerRed1, upperRed1, redMask1);
                        Cv2.InRange(hsvImg, lowerRed2, upperRed2, redMask2);
                        Cv2.BitwiseOr(redMask1, redMask2, colorMask);
                        break;

                    case "schwarz":
                        Scalar lowerBlack = new Scalar(0, 0, 0);
                        Scalar upperBlack = new Scalar(180, 255, 50);
                        Cv2.InRange(hsvImg, lowerBlack, upperBlack, colorMask);
                        break;

                    case "blau":
                        Scalar lowerBlue = new Scalar(100, 50, 50); // Helleres Blau mit mittlerer Sättigung
                        Scalar upperBlue = new Scalar(130, 255, 255); // Dunkleres Blau
                        Cv2.InRange(hsvImg, lowerBlue, upperBlue, colorMask);
                        break;

                    default:
                        throw new ArgumentException("Unbekannte Farbe. Verwenden Sie 'weiß', 'grün', 'rot', 'schwarz' oder 'blau'.");
                }

                // Dilatation zur Verstärkung des Textes
                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new Size(2, 2));
                Cv2.Dilate(colorMask, colorMask, kernel);

                processedImg = colorMask;
            }

            // Speichere das vorverarbeitete Bild für das OCR
            string tempPath = Path.Combine(ScreenshotDirectory, "processed_image.png");
            Cv2.ImWrite(tempPath, processedImg);

            // OCR mit Tesseract
            using var engine = new TesseractEngine(trainedDataDirectory, language, EngineMode.Default);
            engine.SetVariable("debug_file", "NUL"); // Unterdrückt die Debug-Ausgabe von Tesseract
            engine.DefaultPageSegMode = PageSegMode.Auto; // Automatisch für gemischte Texte

            using var pix = Pix.LoadFromFile(tempPath);
            using var page = engine.Process(pix);
            return page.GetText().Trim(); // Entfernt unnötige Leerzeichen am Anfang und Ende
        }


        public bool CheckTextInScreenshot(string textToFind, string textToFind2, string color)
        {
            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", TrainedDataDirectory);
            string text = ProcessImageAndExtract(LocalScreenshotPath, TrainedDataDirectory, color);
            return text.Contains(textToFind) || text.Contains(textToFind2);         
        }

    }
}
