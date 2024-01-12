using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Models
{
    public class SettingsModel
    {
        public int SettingsId { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }

        public SettingsModel(int settingsId, string settingName, string settingValue)
        {
            SettingsId = settingsId;
            SettingName = settingName;
            SettingValue = settingValue;
        }
    }

}
