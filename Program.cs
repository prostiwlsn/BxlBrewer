using BxlConverter;

string inputPath = Console.ReadLine();
string outputPath = Console.ReadLine();

var converter = new BxlBrewer();

var image = converter.PNGToBitmap(inputPath);
var brightnessArray = converter.BitmapToArray(image);
var sequence = converter.ArrayToSequence(brightnessArray, (uint)(image.Width * image.Height));
converter.SequenceToBinary(sequence, outputPath);
