using Raylib_cs;
using System.Numerics;
Raylib.InitWindow(1500, 800, "Topdown game");

Raylib.SetTargetFPS(60);
float speed = 60f;

Color myColor = new Color(255, 105, 180, 255);
Texture2D avatarImage = Raylib.LoadTexture("pixil-frame-0.png");
Rectangle character = new Rectangle(60,60,avatarImage.width,avatarImage.height);

Rectangle enemyRect = new Rectangle(1000,700,64,64);

string currentScene = "start"; //start, game, win, gameover


Vector2 enemyMovement = new Vector2(1,0);
float enemySpeed = 2;


while (Raylib.WindowShouldClose() == false)
{

    //LOGIK
    if (currentScene == "game")
    {
        if(Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            character.x += speed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            character.x -= speed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            character.y -= speed;
        }
        if(Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            character.y  += speed;
        }

        Vector2 playerPos = new Vector2(character.x, character.y);
        Vector2 enemyPos = new Vector2(enemyRect.x,enemyRect.y);
        
        Vector2 diff = playerPos - enemyPos;

        Vector2 enemyDirection = Vector2.Normalize(diff);


        enemyMovement = enemyDirection * enemySpeed;
        
        enemyRect.x += enemyMovement.X;
        enemyRect.y += enemyMovement.Y;

        if(character.x < 0)
        {
            character.x += speed;
        }
        if(character.x > 1500)
        {
            character.x = 1500;
        }
        if(character.y < 0)
        {
            character.y += speed;
        }
        if(character.y > 800)
        {
            character.y = 800;
        }


        if (Raylib.CheckCollisionRecs(character,enemyRect))
        {
            currentScene = "gameover";
        }
    }

    else if (currentScene == "start")
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
        {
            currentScene = "game";
        }
    }
    //GRAFIK
    Raylib.ClearBackground(Color.WHITE);
    Raylib.BeginDrawing();

    if (currentScene == "game")
    {
        Raylib.DrawTexture(avatarImage,
                (int)character.x,
                (int)character.y,
                Color.WHITE
            );
        Raylib.DrawRectangleRec(enemyRect, Color.RED);
    }
    
    else if (currentScene == "start")
    {
        Raylib.DrawText("Press  ENTER to start", 550, 300, 32, Color.BLACK);
    }

    else if (currentScene == "gameover")
    {
        Raylib.DrawText("GAME OVER", 550,300,32, Color.BLACK);
    }

    Raylib.EndDrawing();
}




Console.ReadLine();