using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizationAssignment
{
    public class BEISCH        
    {
        List<Data> valueList = new List<Data>();
        public void ApplyBEISCH(List<int> keyList, int tableSize)
        {

            for(int i=0; i< tableSize; i++)
            {
                Data data = new Data();
                data.Link = -1;
                valueList.Add(data);
            }

            int mode;
            bool bottomOrUp = false;  //false bottom - true up for inserting
            foreach (int key in keyList)
            {
                Data data = new Data(); 
                data.Record = key;
                data.Link = -1; // no links

                mode = GetMode(key, tableSize);

                if(valueList[mode].Record == 0 && valueList[mode].Link == -1)
                {
                    valueList[mode] = data;
                }
                else if(valueList[mode].Record != 0)
                {
                    int previousSecond = -1;
                    int i;
                    if (valueList[mode].Link != -1)
                    {
                        previousSecond = valueList[mode].Link;
                    }
                    if (!bottomOrUp) //search from bottom
                    {
                        for (i = tableSize-1; 0 <= i; i--)
                        {
                            if (valueList[i].Record == 0 && valueList[i].Link == -1)
                            {
                                bottomOrUp = true;
                                valueList[i] = data;
                                valueList[mode].Link = i;
                                break;
                            }
                        }
                    }
                    else      // search from top
                    {
                        for (i = 0; i < tableSize; i++)
                        {
                            if (valueList[i].Record == 0 && valueList[i].Link == -1)
                            {
                                bottomOrUp = false;
                                valueList[i] = data;
                                valueList[mode].Link = i;
                                break;
                            }
                        }
                    }
                    valueList[i].Link = previousSecond;
                }

            }
            PrintBEISCHTable(valueList);
            CalculateProbeNumbers(keyList, valueList, tableSize);

        }

        public int GetMode(int num, int tableSize)

        {
            return (num % tableSize);
        }

        public void PrintBEISCHTable(List<Data> datas)
        {
            
            int tableSize = datas.Count; 

            Console.WriteLine("BEISCH Table:");
            Console.WriteLine("Index\tRecord\tLink");

            for (int i = 0; i < tableSize; i++)
            {
                Data currentData = datas[i];

                // Printing index, record, and link for each entry
                if(datas[i].Record == 0)
                    Console.WriteLine($"{i}\t_\t_");
                else if(datas[i].Link == -1 && datas[i].Record != 0)
                    Console.WriteLine($"{i}\t{currentData.Record}\t_");
                else
                    Console.WriteLine($"{i}\t{currentData.Record}\t{currentData.Link}");
            }
        }

        public void CalculateProbeNumbers(List<int> keyList, List<Data> datas, int tableSize)
        {
            int totalProbe = 0;
            foreach(int key in keyList)
            {
                int probe = 0;
                int mode = GetMode(key, tableSize);

                if(datas[mode].Record == key)
                {
                    probe++;
                }
                else
                {
                    probe++;
                    while(datas[mode].Record != key)
                    {
                        mode = datas[mode].Link;
                        datas[mode].Record = key;
                        probe++;
                    }
                }
                totalProbe = totalProbe + probe;
            }
            double averageProbe = (double)totalProbe / keyList.Count;
            Console.WriteLine($"Avarage probe number for BEISCH: {averageProbe}");
        }

        public void Search(int key, int tableSize)
        {
            int probe = 0;
            int mode = GetMode(key, tableSize);

            if (valueList[mode].Record == key)
            {
                probe++;
            }
            else
            {
                probe++;
                while (valueList[mode].Record != key)
                {
                    mode = valueList[mode].Link;
                    valueList[mode].Record = key;
                    probe++;
                }
            }

            Console.WriteLine($"probe number for {key} is: {probe}");
        }
    }

    public class Data
    {
        public int Record { get; set; }
        public int Link { get; set; }
    }
}
