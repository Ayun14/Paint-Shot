using UnityEngine;
using UnityEngine.Rendering;

public class PaintManager : Singleton<PaintManager>
{
    // 강의를 보며 따라 쓴 코드입니다. 주석으로 설명을 적어놓았습니다.

    public Shader texturePaint;
    public Shader extendIslands;

    // 쉐이더 속성 ID를 캐싱하여 가져옴
    private int prepareUVID = Shader.PropertyToID("_PrepareUV");
    private int positionID = Shader.PropertyToID("_PainterPosition");
    private int hardnessID = Shader.PropertyToID("_Hardness");
    private int strengthID = Shader.PropertyToID("_Strength");
    private int radiusID = Shader.PropertyToID("_Radius");
    private int colorID = Shader.PropertyToID("_PainterColor");
    private int textureID = Shader.PropertyToID("_MainTex");
    private int uvOffsetID = Shader.PropertyToID("_OffsetUV");
    private int uvIslandsID = Shader.PropertyToID("_UVIslands");

    private Material paintMaterial;
    private Material extendMaterial;

    private CommandBuffer command;

    private void Awake()
    {
        // 쉐이더로부터 새로운 메테리얼 생성
        paintMaterial = new Material(texturePaint);
        extendMaterial = new Material(extendIslands);

        // 새로운 명령 버퍼 생성
        command = new CommandBuffer();
    }

    public void initTextures(Paintable paintable)
    {
        // Paintable 객체에서 렌더 텍스처를 가져옴
        RenderTexture mask = paintable.getMask();
        RenderTexture uvIslands = paintable.getUVIslands();
        RenderTexture extend = paintable.getExtend();
        RenderTexture support = paintable.getSupport();
        Renderer rend = paintable.getRenderer();

        // 명령 버퍼에 렌더 타겟을 설정
        command.SetRenderTarget(mask);
        command.SetRenderTarget(extend);
        command.SetRenderTarget(support);

        // 페인트 메테리얼에 준비 상태 플래그 설정
        paintMaterial.SetFloat(prepareUVID, 1);
        command.SetRenderTarget(uvIslands);
        command.DrawRenderer(rend, paintMaterial, 0);

        // 명령 버퍼 실행 및 초기화
        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }

    public void paint(Paintable paintable, Vector3 pos, float radius = 1f, float hardness = .5f, float strength = .5f, Color? color = null)
    {
        // Paintable 객체에서 렌더 텍스처를 가져옴
        RenderTexture mask = paintable.getMask();
        RenderTexture uvIslands = paintable.getUVIslands();
        RenderTexture extend = paintable.getExtend();
        RenderTexture support = paintable.getSupport();
        Renderer rend = paintable.getRenderer();

        // 페인트 메테리얼의 쉐이더 속성을 설정
        paintMaterial.SetFloat(prepareUVID, 0);
        paintMaterial.SetVector(positionID, pos);
        paintMaterial.SetFloat(hardnessID, hardness);
        paintMaterial.SetFloat(strengthID, strength);
        paintMaterial.SetFloat(radiusID, radius);
        paintMaterial.SetTexture(textureID, support);
        paintMaterial.SetColor(colorID, color ?? Color.red);

        extendMaterial.SetFloat(uvOffsetID, paintable.extendsIslandOffset);
        extendMaterial.SetTexture(uvIslandsID, uvIslands);

        // 명령 버퍼에 렌더 타겟 및 드로우 호출을 추가
        command.SetRenderTarget(mask);
        command.DrawRenderer(rend, paintMaterial, 0);

        // 마스크를 support 텍스처로 복사
        command.SetRenderTarget(support);
        command.Blit(mask, support);

        // 마스크를 확장 텍스처로 복사하고 확장 메테리얼을 사용
        command.SetRenderTarget(extend);
        command.Blit(mask, extend, extendMaterial);

        // 명령 버퍼 실행 및 초기화
        Graphics.ExecuteCommandBuffer(command);
        command.Clear();
    }
}
