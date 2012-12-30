using System;
namespace Glib
{
    /// <summary>
    /// Matametika.
    /// </summary>
    public static class GMath
    {
        #region Konstanty

        /// <summary>
        /// Jedna polovina (1/2).
        /// </summary>
        public const float _12 = 0.5f;

        /// <summary>
        /// Jedna čtvrtina (1/4).
        /// </summary>
        public const float _14 = 0.25f;

        /// <summary>
        /// Jedna osmina (1/8).
        /// </summary>
        public const float _18 = 0.125f;

        /// <summary>
        /// Tři čtvrtiny (3/4).
        /// </summary>
        public const float _34 = 0.75f;

        /// <summary>
        /// Konstanta PI.
        /// </summary>
        public const float PI = 3.141593f;

        /// <summary>
        /// Konstance PI * 2.
        /// </summary>
        public const float TwoPI = PI * 2.0f;

        /// <summary>
        /// Konstance PI * 3.
        /// </summary>
        public const float ThreePI = PI * 3.0f;

        /// <summary>
        /// Konstance PI * 4.
        /// </summary>
        public const float FourPI = PI * 4.0f;

        /// <summary>
        /// Konstance půlky PI (PI / 2).
        /// </summary>
        public const float HalfPI = PI / 2.0f;

        /// <summary>
        /// Sto osumdesátina PI (cca 0.017).
        /// </summary>
        public const float PI_180 = PI / 180.0f;

        /// <summary>
        /// Konstanta jednoho radiánu (PI / 180).
        /// </summary>
        public const float Radian = 180.0f / PI;

        #endregion Konstanty

        /// <summary>
        /// Vypočte sínus.
        /// </summary>
        /// <param name="value">Hodnota.</param>
        /// <returns>Vrací sínus.</returns>
        public static float Sin(double value)
        {
            return (float)Math.Sin(value);
        }

        /// <summary>
        /// Vypočte kosínus.
        /// </summary>
        /// <param name="value">Hodnota.</param>
        /// <returns>Vrací kosínus.</returns>
        public static float Cos(double value)
        {
            return (float)Math.Cos(value);
        }

        /// <summary>
        /// Vypočte druhou odmocninu.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Vrací druhou odmocninu.</returns>
        public static float Sqrt(double value)
        {
            return (float)Math.Sqrt(value);
        }
    }
}
