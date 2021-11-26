using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using Microsoft.Xna.Framework;

namespace StoresAnywhere
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.MenuChanged += UpdatePhone;
        }

        private Vector2 playerTile;
        private GameLocation playerLocation;
        private void UpdatePhone(object sender, MenuChangedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            //if our last menu was building or animal purchase, warp the farmer back to their original location upon exit
            if (e.OldMenu is CarpenterMenu || e.OldMenu is PurchaseAnimalsMenu)
            {
                //if the player uses the shop from it's actual location, don't warp
                if (playerLocation.name.Value is "ScienceHouse" || playerLocation.name.Value is "AnimalShop")
                    return;

                Game1.warpFarmer(playerLocation.name, (int)playerTile.X, (int)playerTile.Y, Game1.player.facingDirection);
            }

            //return if we are not in any of the telephone menus
            if (!(e.NewMenu is ShopMenu) && !(e.NewMenu is CarpenterMenu) && !(e.NewMenu is PurchaseAnimalsMenu))
                return;
            
            if (e.NewMenu is ShopMenu menu)
            {
                menu.readOnly = false;
                return;
            }

            playerTile = Game1.player.getTileLocation();
            playerLocation = Game1.player.currentLocation;

            if (e.NewMenu is PurchaseAnimalsMenu aMenu)
            {
                aMenu.readOnly = false;
                return;
            }

            if (e.NewMenu is CarpenterMenu cMenu)
            {
                cMenu.readOnly = false;
                cMenu.upgradeIcon.visible = true;
                cMenu.demolishButton.visible = true;
                cMenu.moveButton.visible = true;
                cMenu.okButton.visible = true;
                cMenu.paintButton.visible = true;
                return;
            }
        }
    }
}