using System;

namespace VamTimeline
{
    /// <summary>
    /// VaM Timeline
    /// By Acidbubbles
    /// Animation timeline with keyframes
    /// Source: https://github.com/acidbubbles/vam-timeline
    /// </summary>
    public class BackupPlugin : MVRScript
    {
        public override void Init()
        {
            try
            {
                {
                    var backupJSON = new JSONStorableString(StorableNames.AtomAnimationBackup, "");
                    RegisterString(backupJSON);
                    var textfield = CreateTextField(backupJSON);
                    textfield.height = 1200;
                }

                {
                    var backupJSON = new JSONStorableString(StorableNames.MorphsAnimationBackup, "");
                    RegisterString(backupJSON);
                    var textfield = CreateTextField(backupJSON);
                    textfield.height = 1200;
                }
            }
            catch (Exception exc)
            {
                SuperController.LogError("VamTimeline.BackupPlugin.Init: " + exc);
            }
        }
    }
}
