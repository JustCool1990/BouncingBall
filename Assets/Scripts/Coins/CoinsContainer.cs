using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsContainer : MonoBehaviour
{
    [SerializeField] private List<CoinGroup> _coinGroups;
    [SerializeField] private List<ColorAtribute> _colorAtributes;

    private void Awake()
    {
        ShuffleColorAtributes();
    }

    public void SetCoinsColorAtributes()
    {
        for (int i = 0; i < _coinGroups.Count; i++)
        {
            _coinGroups[i].SetColorAtributes(_colorAtributes[i]);
        }
    }

    public void ShuffleColorAtributes()
    {
        for (int i = _colorAtributes.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i);

            var tmp = _colorAtributes[j];
            _colorAtributes[j] = _colorAtributes[i];
            _colorAtributes[i] = tmp;
        }
    }
}

[System.Serializable]
public class CoinGroup
{
    [SerializeField] private string _label;
    [SerializeField] private Coin[] _coins;

    public void SetColorAtributes(ColorAtribute colorAtribute)
    {
        foreach (var coin in _coins)
        {
            if (coin.isActiveAndEnabled == false)
            {
                ShowHidedCoin(coin);
            }

            coin.SetColorAtribute(colorAtribute);
        }
    }

    private void ShowHidedCoin(Coin coin)
    {
        coin.transform.rotation = Quaternion.identity;
        coin.gameObject.SetActive(true);
    }
}
