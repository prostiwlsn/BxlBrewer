using BxlConverter;

string inputPath = Console.ReadLine();
string outputPath = Console.ReadLine();

var converter = new BxlBrewer();

var image = converter.PNGToBitmap(inputPath);
var brightnessArray = converter.BitmapToArray(image);
var sequence = converter.ArrayToSequence(brightnessArray, image.Height, image.Width);
converter.SequenceToBinary(sequence, outputPath);