using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace semiFinalACT3
{
    public class table
    {
        [PrimaryKey, AutoIncrement]
        public int meterNo { get; set; }
        public double presR {  get; set; }
        public double prevR { get; set; }
        public string typeOfR {  get; set; }
        public double principalAmount{ get; set; }
        public double amountPayable { get; set; }
    }
}
