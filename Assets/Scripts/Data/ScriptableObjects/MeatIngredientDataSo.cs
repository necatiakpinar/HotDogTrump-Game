using Abstracts;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MeatIngredientData", menuName = "Data/ScriptableObjects/IngredientData/MeatIngredientData")]
    public class MeatIngredientDataSo : BaseIngredientDataSo
    {
        [SerializeField] private Color _rawColor = new Color(1, 0.4f, 0.4f); 
        [SerializeField] private Color _mediumCookColor = new Color(0.8f, 0.3f, 0.2f);
        [SerializeField] private Color _readyCookColor = new Color(0.6f, 0.2f, 0.1f);
        [SerializeField] private Color _burntColor = new Color(0.2f, 0.15f, 0.1f);
        
        [SerializeField] private float _rawCookTime = 3f;
        [SerializeField] private float _mediumCookTime = 5f;
        [SerializeField] private float _readyCookTime = 7f;
        [SerializeField] private float _burntCookTime = 9f;
        
        public Color RawColor => _rawColor;
        public Color MediumCookColor => _mediumCookColor;
        public Color ReadyCookColor => _readyCookColor;
        public Color BurntColor => _burntColor;
        
        public float RawCookTime => _rawCookTime;
        public float MediumCookTime => _mediumCookTime;
        public float ReadyCookTime => _readyCookTime;
        public float BurntCookTime => _burntCookTime;
    }
}