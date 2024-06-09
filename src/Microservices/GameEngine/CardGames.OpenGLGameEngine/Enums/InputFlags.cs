﻿namespace CardGames.OpenGLGameEngine.Enums
{
    public enum InputFlags
    {
        None = 0,
        Player = 1 << 0,
        Menu = 1 << 1,
        Chat = 1 << 2,
        Options = 1 << 3,




        Reset = Player | Menu | Chat,
        All = Reset | Options
    }
}
