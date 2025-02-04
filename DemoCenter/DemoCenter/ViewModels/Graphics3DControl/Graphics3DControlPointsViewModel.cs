using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls3D;

namespace DemoCenter.ViewModels;

public partial class Graphics3DControlPointsViewModel : Graphics3DControlViewModel
{
    class Arm
    {
        float Center { get; }
        float HalfAngle { get; }
        public float Min { get; }
        public float Max { get; }
        public float Length { get; }

        public Arm(float min, float max, float length)
        {
            Min = min;
            Max = max;
            Length = length;
            Center = 0.5f * (Max + Min);
            HalfAngle = 0.5f * (Max - Min);
        }
        public Vector2 GetRadiusAndElevation(Random random, float minRadius, float maxElevation, float angle)
        {
            float factorR = 1f - MathF.Abs((Center - angle) / HalfAngle);
            float radius = random.NextSingle() * ((1f - factorR * factorR) * Length + minRadius);
            float factorE = radius / (minRadius + Length);
            float elevation = (random.NextSingle() - 0.5f) * (1f - factorE * factorE * factorE) * maxElevation * 2f;
            return new Vector2(radius, elevation);
        }
    }
    
    const int StarsCount = 5_000_000;
    const float LargeArmRadius = 0.7f;
    const float SmallArmRadius = 0.5f;
    const float CenterRadius = 0.2f;
    const float MaxElevation = 0.1f;
    const float NoiseXY = MaxElevation;
    const float NoiseZ = MaxElevation * 0.5f;
    const float MaxRadius = CenterRadius + LargeArmRadius;
    
    static uint[] CreateIndices(int count)
    {
        var result = new uint[count];
        for (uint i = 0; i < result.Length; i++)
            result[i] = i;
        return result;
    }

    [ObservableProperty] ObservableCollection<MeshGeometry3D> meshes = new();
    [ObservableProperty] float pointSize;
    [ObservableProperty] Bitmap emissionImage;
    [ObservableProperty] Bitmap albedoImage;

    public Graphics3DControlPointsViewModel()
    {
        var assembly = Assembly.GetAssembly(typeof(Graphics3DControlViewModel));
        var stream = assembly!.GetManifestResourceStream("DemoCenter.Resources.Graphics3D.Textures.Galaxy.png");
        EmissionImage = new Bitmap(stream!);
        var albedo = new RenderTargetBitmap(EmissionImage.PixelSize);
        using (var context = albedo.CreateDrawingContext())
        {
            context.FillRectangle(Brushes.Black, new Rect(0, 0, EmissionImage.PixelSize.Width, EmissionImage.PixelSize.Height));
        }
        AlbedoImage = albedo;

        var vertices = new Vertex3D[StarsCount];
        var arms = new Dictionary<int, Arm>();
        arms.Add(0, new Arm(0, 0.5f * MathF.PI, LargeArmRadius));
        arms.Add(1, arms[0]);
        arms.Add(2, new Arm(0.5f * MathF.PI, 0.75f * MathF.PI, SmallArmRadius));
        arms.Add(3, new Arm(0.75f * MathF.PI, MathF.PI, SmallArmRadius));
        arms.Add(4, new Arm(MathF.PI, 1.5f * MathF.PI, LargeArmRadius));
        arms.Add(5, arms[4]);
        arms.Add(6, new Arm(1.5f * MathF.PI, 1.75f * MathF.PI, SmallArmRadius));
        arms.Add(7, new Arm(1.75f * MathF.PI, 2f * MathF.PI, SmallArmRadius));
        
        var random = new Random(42);
        for (int i = 0; i < StarsCount; i++)
        {
            float angle = random.NextSingle() * MathF.PI * 2f;
            int octave = (int)MathF.Min(arms.Count - 1, MathF.Floor(angle / (MathF.PI * 0.25f)));
            var vec = arms[octave].GetRadiusAndElevation(random, CenterRadius, MaxElevation, angle);
            float radius = vec.X;
            angle += vec.X / MaxRadius * MathF.PI * 1.5f;
            float x = MathF.Cos(angle) * radius + random.NextSingle() * NoiseXY;
            float z = MathF.Sin(angle) * radius + random.NextSingle() * NoiseXY;
            float y = random.NextSingle() * vec.Y + random.NextSingle() * NoiseZ;

            float textureU = MathF.Max(0f, MathF.Min(1f, radius / MaxRadius + (random.NextSingle() - 0.5f) * 0.25f));
            
            vertices[i] = new Vertex3D
            {
                Position = new Vector3(x, y, z),
                TextureCoord = new Vector2(textureU, 0)
            };
        }

        Meshes.Add(new MeshGeometry3D
        {
            FillType = MeshFillType.Points,
            Vertices = vertices,
            Indices = CreateIndices(StarsCount),
            MaterialKey = "material"
        });
    }
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(PointSize))
        {
            foreach (var mesh in Meshes)
                mesh.PrimitiveSize = PointSize;
        }
    }
}
