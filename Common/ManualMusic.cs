using Terraria.ModLoader;

namespace RealmOne.Common
{
    
    public sealed class ManualMusic : ILoadable
    {
        public void Load(Mod mod)
        {
            

            MusicLoader.AddMusic(mod, "Assets/Music/squirmointro");
            MusicLoader.AddMusic(mod, "Assets/Music/Rlyeh");
            MusicLoader.AddMusic(mod, "Assets/Music/PiggyPatrol");
            MusicLoader.AddMusic(mod, "Assets/Music/CottageOrchestra");


        }

        public void Unload() 
        {
      
        }
    }
}

