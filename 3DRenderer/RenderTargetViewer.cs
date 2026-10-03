namespace _3DRenderer;

public class RenderTargetViewer<T>
{
    private RenderTarget<T> _target;
    private Texture2D _texture;
    private Color[] _pixelBuffer;

    private string _widthText;
    private string _heightText;
    private bool _editingWidth;   // = false;
    private bool _editingHeight;  // = false;

    private float _viewportScale = 1.0f;

    private const int DefaultWindowWidth = 1200;
    private const int DefaultWindowHeight = 800;
    private const int PanelWidth = 260;

    public RenderTargetViewer(RenderTarget<T> target)
    {
        _target = target;
        _widthText = target.Width.ToString();
        _heightText = target.Height.ToString();
        _pixelBuffer = new Color[target.Width * target.Height];
    }

    public void Run()
    {
        Raylib.SetConfigFlags(ConfigFlags.HighDpiWindow | ConfigFlags.ResizableWindow);
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, "3D Renderer");
        Raylib.SetTargetFPS(60);

        RecreateTexture();

        while (!Raylib.WindowShouldClose())
        {
            int currentScreenWidth = Raylib.GetScreenWidth();
            int currentScreenHeight = Raylib.GetScreenHeight();
            int viewportWidth = Math.Max(10, currentScreenWidth - PanelWidth);

            UpdatePixelBuffer();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(20, 20, 20, 255));
            
            Raylib.UpdateTexture(_texture, _pixelBuffer);
            
            float targetAspect = (float)_target.Width / _target.Height;
            float viewportAspect = (float)viewportWidth / currentScreenHeight;

            float baseDrawWidth, baseDrawHeight;

            if (targetAspect > viewportAspect)
            {
                baseDrawWidth = viewportWidth;
                baseDrawHeight = viewportWidth / targetAspect;
            }
            else
            {
                baseDrawHeight = currentScreenHeight;
                baseDrawWidth = currentScreenHeight * targetAspect;
            }

            float drawWidth = baseDrawWidth * _viewportScale * 0.95f;
            float drawHeight = baseDrawHeight * _viewportScale * 0.95f;

            float drawX = (viewportWidth - drawWidth) / 2f;
            float drawY = (currentScreenHeight - drawHeight) / 2f;

            Rectangle sourceRec = new Rectangle(0, 0, _target.Width, _target.Height);
            Rectangle destRec = new Rectangle(drawX, drawY, drawWidth, drawHeight);

            Raylib.BeginScissorMode(0, 0, viewportWidth, currentScreenHeight);
            Raylib.DrawTexturePro(_texture, sourceRec, destRec, Vector2.Zero, 0f, Color.White);
            Raylib.EndScissorMode();
            
            DrawControlPanel(viewportWidth, currentScreenHeight);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(_texture);
        Raylib.CloseWindow();
    }

    private void DrawControlPanel(int viewportWidth, int screenHeight)
    {
        int panelX = viewportWidth;

        //sidebar
        Raylib.DrawRectangle(panelX, 0, PanelWidth, screenHeight, new Color(50,50,50,255));
        Raylib.DrawLine(panelX, 0, panelX, screenHeight, Color.Black);

        int yOffset = 20;

        //buffer size controls
        Raylib.DrawText("Render Resolution", panelX + 20, yOffset, 18, Color.RayWhite);
        yOffset += 40;

        //width
        Raylib.DrawText("Width:", panelX + 20, yOffset, 16, Color.White);
        yOffset += 25;
        _widthText = DrawTextBox(panelX + 20, yOffset, 220, 32, _widthText, ref _editingWidth, ref _editingHeight);
        yOffset += 45;

        //height
        Raylib.DrawText("Height:", panelX + 20, yOffset, 16, Color.White);
        yOffset += 25;
        _heightText = DrawTextBox(panelX + 20, yOffset, 220, 32, _heightText, ref _editingHeight, ref _editingWidth);
        yOffset += 45;

        //apply
        if (DrawButton("Apply Size", panelX + 20, yOffset, 220, 35))
        {
            ApplyNewDimensions();
        }
        yOffset += 55;

        //separator
        Raylib.DrawLine(panelX + 10, yOffset, panelX + PanelWidth - 10, yOffset, Color.DarkGray);
        yOffset += 20;

        //zoom
        Raylib.DrawText("Viewport Zoom", panelX + 20, yOffset, 18, Color.RayWhite);
        yOffset += 30;

        Raylib.DrawText($"Zoom: {(_viewportScale * 100):F0}%", panelX + 20, yOffset, 16, Color.White);
        yOffset += 25;

        _viewportScale = Math.Clamp(DrawSlider(panelX + 20, yOffset, 220, 20, _viewportScale, 0.45f, 3.05f), 0.5f, 3.0f);
        yOffset += 30;
        
        if (DrawButton("100%", panelX + 20, yOffset, 65, 30)) _viewportScale = 1.0f;
    }

    private string DrawTextBox(int x, int y, int width, int height, string text, ref bool isActive, ref bool otherActive)
    {
        Rectangle rect = new Rectangle(x, y, width, height);
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            if (Raylib.CheckCollisionPointRec(mousePos, rect))
            {
                isActive = true;
                otherActive = false;
            }
            else
            {
                isActive = false;
            }
        }

        if (isActive)
        {
            int key = Raylib.GetCharPressed();
            while (key > 0)
            {
                if (key >= 48 && key <= 57 && text.Length < 5) //0->9
                {
                    text += (char)key;
                }
                key = Raylib.GetCharPressed();
            }
            
            if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && text.Length > 0)
            {
                text = text[..^1];
            }
            
            if (Raylib.IsKeyPressed(KeyboardKey.Enter) || Raylib.IsKeyPressed(KeyboardKey.KpEnter))
            {
                isActive = false;
                ApplyNewDimensions();
            }
        }

        Color boxColor = isActive ? Color.RayWhite : Color.LightGray;
        Raylib.DrawRectangleRec(rect, boxColor);
        Raylib.DrawRectangleLinesEx(rect, isActive ? 2 : 1, isActive ? Color.Blue : Color.Black);
        
        string displayText = text + (isActive && ((int)(Raylib.GetTime() * 2) % 2 == 0) ? "|" : "");
        Raylib.DrawText(displayText, x + 8, y + (height - 16) / 2, 16, Color.Black);

        return text;
    }

    private bool DrawButton(string text, int x, int y, int width, int height)
    {
        Rectangle rect = new Rectangle(x, y, width, height);
        Vector2 mousePos = Raylib.GetMousePosition();
        bool isHovered = Raylib.CheckCollisionPointRec(mousePos, rect);

        Color btnColor = isHovered ? new Color(150, 150, 150, 255) : Color.DarkGray;
        Raylib.DrawRectangleRec(rect, btnColor);
        Raylib.DrawRectangleLinesEx(rect, 2, Color.Black);

        int textWidth = Raylib.MeasureText(text, 16);
        Raylib.DrawText(text, x + (width - textWidth) / 2, y + (height - 16) / 2, 16, Color.White);

        return isHovered && Raylib.IsMouseButtonPressed(MouseButton.Left);
    }

    private float DrawSlider(int x, int y, int width, int height, float value, float min, float max)
    {
        Rectangle trackRect = new Rectangle(x, y, width, height);
        Vector2 mousePos = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonDown(MouseButton.Left) && Raylib.CheckCollisionPointRec(mousePos, trackRect))
        {
            float clampX = Math.Clamp(mousePos.X - x, 0f, width);
            value = min + (clampX / width) * (max - min);
        }

        Raylib.DrawRectangleRec(trackRect, Color.DarkGray);
        Raylib.DrawRectangleLinesEx(trackRect, 1, Color.Black);

        float handleNormalized = (value - min) / (max - min);
        int handleX = x + (int)(handleNormalized * width);
        Rectangle handleRect = new Rectangle(handleX - 5, y - 2, 10, height + 4);

        bool isHandleHovered = Raylib.CheckCollisionPointRec(mousePos, handleRect);
        Raylib.DrawRectangleRec(handleRect, isHandleHovered ? Color.White : Color.LightGray);
        Raylib.DrawRectangleLinesEx(handleRect, 1, Color.Black);

        return value;
    }

    private void ApplyNewDimensions()
    {
        if (int.TryParse(_widthText, out int w) && int.TryParse(_heightText, out int h))
        {
            w = Math.Clamp(w, 10, 4096);
            h = Math.Clamp(h, 10, 4096);

            _widthText = w.ToString();
            _heightText = h.ToString();

            ResizeTarget(w, h);
        }
    }

    private void ResizeTarget(int width, int height)
    {
        _target.Resize(width, height);
        _pixelBuffer = new Color[width * height];

        Raylib.UnloadTexture(_texture);
        RecreateTexture();
    }

    private void RecreateTexture()
    {
        Image img = Raylib.GenImageColor(_target.Width, _target.Height, Color.Black);
        _texture = Raylib.LoadTextureFromImage(img);
        Raylib.UnloadImage(img);
    }

    private void UpdatePixelBuffer()
    {
        for (int y = 0; y < _target.Height; y++)
        {
            for (int x = 0; x < _target.Width; x++)
            {
                T? value = _target.Buffer[x, y];
                _pixelBuffer[y * _target.Width + x] = ConvertToColor(value);
            }
        }
    }

    private static Color ConvertToColor(T? value)
    {
        if (value is null) return Color.Black;

        return value switch
        {
            bool b => b ? Color.White : Color.Black,
            int i => new Color((byte)Math.Clamp(i, 0, 255), (byte)Math.Clamp(i, 0, 255), (byte)Math.Clamp(i, 0, 255), (byte)255),
            Vector2 v2 => new Color(NormalizeToByte(v2.X), NormalizeToByte(v2.Y), (byte)0, (byte)255),
            Vector3 v3 => new Color(NormalizeToByte(v3.X), NormalizeToByte(v3.Y), NormalizeToByte(v3.Z), (byte)255),
            Vector4 v4 => BlendOverBlack(NormalizeToByte(v4.X), NormalizeToByte(v4.Y), NormalizeToByte(v4.Z), NormalizeToByte(v4.W)),
            _ => Color.Black
        };
    }

    private static byte NormalizeToByte(float val)
    {
        float clamped = val > 1.0f ? Math.Clamp(val, 0f, 255f) : Math.Clamp(val * 255f, 0f, 255f);
        return (byte)clamped;
    }

    private static Color BlendOverBlack(byte r, byte g, byte b, byte a)
    {
        float alpha = a / 255.0f;
        return new Color((byte)(r * alpha), (byte)(g * alpha), (byte)(b * alpha), (byte)255);
    }
}