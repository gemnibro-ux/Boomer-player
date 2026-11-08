using System.Numerics;
using Raylib_cs;

namespace boomer;

public class Player
{
    static int w = 600;
    static int h = 400;
    static bool toogle_a = false;
    static bool toogle_b = true;
    static float rotation = 0f;
    static Color pause_color = Color.Blue;

    public static void Main()
    {
        Rectangle pause = new Rectangle(0, h - 50, 50, 50);
        Vector2 pointer = Raylib.GetMousePosition();
        Camera3D _camera = new Camera3D();
        _camera.Position = new Vector3(0f, 5f, 15f);
        _camera.Target = new Vector3(0, 1.0f, 1.5f);
        _camera.Up = Vector3.UnitZ;
        _camera.FovY = 60f;

        Raylib.InitWindow(w, h, "boomer");
        Model cube = Raylib.LoadModel("cube.glb");
        RenderTexture2D canvas = Raylib.LoadRenderTexture(w, h - 50);
        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.BeginTextureMode(canvas);

            Raylib.BeginMode3D(_camera);
            Raylib.ClearBackground(Color.Black);
            if (toogle_b) {rotation++;} else {}
            Raylib.DrawModelEx(cube, new Vector3(0, 5f, 10f), new Vector3(1f, 0, 0), rotation, new Vector3(2f, 2, 2f), Color.Red);
            Raylib.EndMode3D();

            Raylib.EndTextureMode();

            Raylib.DrawTexturePro(canvas.Texture, new Rectangle(0, 0, canvas.Texture.Width, -canvas.Texture.Height), new Rectangle(w/2, (h - 50) / 2, canvas.Texture.Width, canvas.Texture.Height), new Vector2(w/2, 350 / 2), 0, Color.White);
            Raylib.DrawRectangleRec(pause, pause_color);
            if (!Raylib.CheckCollisionPointRec(pointer, pause) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (!toogle_a)
                {
                    toogle_a = true;
                    toogle_b = false;
                    pause_color = Color.Red;
                }
                else
                {
                    toogle_a = false;
                    toogle_b = true;
                    pause_color = Color.Blue;
                }
            }
            Raylib.DrawText("0.1v", w - Raylib.MeasureText("0.1v", 12) - 60, h - 20, 12, Color.Black);

            Raylib.EndDrawing();
        }
    }
}
