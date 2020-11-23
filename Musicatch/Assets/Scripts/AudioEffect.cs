using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2f;
    Vector3 initSize;
    float[] sample = new float[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int sampleIndex = 0;
    // Start is called before the first frame update

    void Start()
    {
        initSize = gameObject.transform.localScale;
    }
    void Update()
    {
        float[] spectrum = new float[256];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float sum = 0;
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            sum += spectrum[i];
            // Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            // Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            // Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            // Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
        sample[sampleIndex++ % 10] = sum;
        float sampleAvg = 0;
        for (int i = 0; i < 10; i++)
        {
            sampleAvg += sample[i];
        }
        sampleAvg /= 10;
        sum = sum / sampleAvg;//현재값이 평균보다 큰지 작은지 0~2배 까지 확인
        if (sum > 2f) sum = 2f;
        sum = sum / 2 * maxSize;
        if (sum < minSize) sum = minSize;
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(initSize.x * sum, initSize.y * sum, 1), 10f * Time.deltaTime);
    }
}
