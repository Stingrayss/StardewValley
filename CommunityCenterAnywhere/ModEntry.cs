using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using Microsoft.Xna.Framework;

namespace CommunityCenterAnywhere
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += OnMenuUpdate;
        }
        private void OnMenuUpdate(object sender, MenuChangedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (e.NewMenu is JunimoNoteMenu menu)
            {
                foreach (Bundle bundle in menu.bundles)
                {
                    if (bundle.name is "2,500g" || bundle.name is "5,000g" || bundle.name is "10,000g" || bundle.name is "25,000g")
                        menu.purchaseButton = new ClickableTextureComponent(new Rectangle(menu.xPositionOnScreen + 800, menu.yPositionOnScreen + 504, 260, 72), menu.noteTexture, new Rectangle(517, 286, 65, 20), 4f);

                    bundle.depositsAllowed = true;
                }
            }
        }
    }
}