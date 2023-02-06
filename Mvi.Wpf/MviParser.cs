using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvi.Wpf
{
    public class MviParser
    {
        // input => 32長度
        public MeasureReuslt MeasureParse(string input)
        {
            var inputList = input.Split(',').ToList();
            inputList.Remove(inputList.First());

            var data = inputList.ConvertAll(Convert.ToInt32);
            MeasureReuslt result = new MeasureReuslt()
            {
                PosNo = Convert.ToInt32(Convert.ToString(data[6], 16)),
                TCS = Convert.ToString(data[8], 16).ToUpper(),
                Diameter = Convert.ToInt32(Convert.ToString(data[10], 16)),
                Weight = Convert.ToInt32(Convert.ToString(data[20], 16)),
            };

            return result;
        }

        // input => 13長度
        public LevelResult LevelParse(string input)
        {
            var last = Convert.ToInt32(input.Split(',').Last());
            var data = Convert.ToString(last, 2).ToCharArray().Reverse().Take(8).ToList();

            if (data.Count() == 1)
            {
                return new LevelResult();
            }

            LevelResult result = new LevelResult()
            {
                AM = data[0] == '1',
                AN = data[1] == '1',
                B = data[3] == '1',
                Bs = data[4] == '1',
                C = data[6] == '1',
                D = data[7] == '1',
            };

            return result;
        }
    }
}
