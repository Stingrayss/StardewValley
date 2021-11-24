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
            helper.Events.Display.MenuChanged += this.updatePhone;
        }

        private Vector2 playerTile;
        private GameLocation playerLocation;
        private void updatePhone(object sender, MenuChangedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                return;
            }
            if (e.OldMenu is CarpenterMenu || e.OldMenu is PurchaseAnimalsMenu)
            {
                Game1.warpFarmer(playerLocation.name, (int)playerTile.X, (int)playerTile.Y, Game1.player.facingDirection);
            }
            if (Game1.activeClickableMenu is ShopMenu menu)
            {
                menu.readOnly = false;
                return;
            }

            playerTile = Game1.player.getTileLocation();
            playerLocation = Game1.player.currentLocation;

            if (Game1.activeClickableMenu is PurchaseAnimalsMenu aMenu)
            {
                aMenu.readOnly = false;
                return;
            }
            if (Game1.activeClickableMenu is CarpenterMenu cMenu)
            {
                cMenu.readOnly = false;
                cMenu.upgradeIcon.visible = true;
                cMenu.demolishButton.visible = true;
                cMenu.moveButton.visible = true;
                cMenu.okButton.visible = true;
                cMenu.paintButton.visible = true;
            }
        }
    }
}
