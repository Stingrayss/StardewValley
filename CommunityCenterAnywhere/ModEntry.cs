using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using Microsoft.Xna.Framework;

namespace CommunityCenterAnywhere
{
    public class ModEntry : Mod
    {

        public static ModConfig Config;
        public override void Entry(IModHelper helper)
        {
            Config = Helper.ReadConfig<ModConfig>();
            if (!Config.EnableMod)
                return;
            helper.Events.Display.MenuChanged += OnMenuUpdate;
        }

        private Vector2 playerTile;
        private GameLocation playerLocation;
        private void OnMenuUpdate(object sender, MenuChangedEventArgs e)
        {

            if (!Context.IsWorldReady)
                return;

            //warp the player back to their original location when they are done
            if (e.OldMenu is JunimoNoteMenu && (!(e.NewMenu is JunimoNoteMenu)) && (!(playerLocation.name.Value is "CommunityCenter")) && Config.PlayerWarping)
                Game1.warpFarmer(playerLocation.name, (int)playerTile.X, (int)playerTile.Y, Game1.player.facingDirection);

            if (e.NewMenu is JunimoNoteMenu menu)
            {
                //update player location only if they have not previously been in the Community Center menu
                if (!(e.OldMenu is JunimoNoteMenu))
                {
                    playerTile = Game1.player.getTileLocation();
                    playerLocation = Game1.player.currentLocation;
                }

                //warp the player to the Community Center so that bundle completion will be counted
                if (!(Game1.player.currentLocation.name.Value is "CommunityCenter") && Config.PlayerWarping)
                    Game1.warpFarmer(Game1.getLocationFromName("CommunityCenter").name, 32, 18, Game1.player.facingDirection);

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