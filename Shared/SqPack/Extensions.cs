using System;
using System.Linq;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace Shared.SqPack
{
    public static class Extensions
    {
        public static bool TryGetValue<T>(this ExcelSheet<T> sheet, uint key, out T entry) where T : ExcelRow
        {
            try
            {
                entry = sheet.GetRow(key);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                entry = null;
                return false;
            }
        }
        
        public static string GetBgName(this TerritoryType territoryType)
        {
            return territoryType.Bg.RawString.Split('/').Last();
        }
    }
}
