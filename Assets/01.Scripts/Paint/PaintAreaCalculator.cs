using UnityEngine;

public class PaintAreaCalculator : MonoBehaviour
{
    [SerializeField] private PaintManager paintManager;

    // 이 메서드는 물감이 칠해진 면적의 비율을 계산합니다.
    public float CalculatePaintCoverage(Paintable paintable)
    {
        // Paintable 객체에서 마스크 텍스처를 가져옵니다.
        RenderTexture mask = paintable.getMask();

        // 마스크 텍스처를 읽을 수 있게 만듭니다.
        RenderTexture.active = mask;

        // RenderTexture에서 픽셀을 읽기 위해 새로운 Texture2D를 생성합니다.
        Texture2D readableTexture = new Texture2D(mask.width, mask.height, TextureFormat.RGB24, false);
        readableTexture.ReadPixels(new Rect(0, 0, mask.width, mask.height), 0, 0);
        readableTexture.Apply();

        RenderTexture.active = null;

        // 읽을 수 있는 텍스처에서 픽셀 데이터를 가져옵니다.
        Color[] pixels = readableTexture.GetPixels();

        // 칠해진 픽셀을 셉니다.
        int paintedPixelCount = 0;
        foreach (Color pixel in pixels)
        {
            // 칠해진 영역이 투명하지 않다고 가정합니다.
            if (pixel.a > 0)
            {
                paintedPixelCount++;
            }
        }

        // 총 픽셀 수를 계산합니다.
        int totalPixels = pixels.Length;

        // 칠해진 면적의 비율을 계산합니다.
        float coverage = (float)paintedPixelCount / totalPixels * 100;

        // 정리
        Destroy(readableTexture);

        return coverage;
    }
}
