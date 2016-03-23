using System.Collections.Generic;

namespace OpenIso8583Net
{
    /// <summary>
    ///   This class parses Ministatement data in field 48 in the response message
    /// </summary>
    public class MinistatementData : List<MinistatementLine>
    {
        /// <summary>
        ///   Parse the data out of the message
        /// </summary>
        /// <param name = "msg">Data to parse</param>
        public void FromMsg(string msg)
        {
            // Headers

            var lines = msg.Split('~');

            var header = lines[0];
            var headings = header.Split('|');

            // Through each line
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split('|');
                if (parts.Length == 1) // If the line is empty, don't bother
                    continue;

                var msLine = new MinistatementLine();
                for (var j = 0; j < headings.Length; j++)
                {
                    var heading = headings[j];
                    if (j < parts.Length)
                    {
                        var data = parts[j];
                        if (string.IsNullOrEmpty(data))
                            data = null;

                        msLine.Add(heading, data);
                    }
                    else
                        msLine.Add(heading, null);
                }
                Add(msLine);
            }
        }
    }
}