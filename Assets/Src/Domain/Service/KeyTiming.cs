namespace Assets.Src.Domain.Service
{
    /// <summary>
    /// キーの押下状態種別
    /// </summary>
    public enum KeyTiming
    {
        /// <summary>
        /// 今押した
        /// </summary>
        DOWN,
        /// <summary>
        /// 押している
        /// </summary>
        ON,
        /// <summary>
        /// 今押すのを止めた
        /// </summary>
        UP,
        /// <summary>
        /// 押していない
        /// </summary>
        OFF,
    }
}
