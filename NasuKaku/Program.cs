using System;

namespace NasuKaku
{
    class Program
    {
        /// <summary>
        /// 参考：https://github.com/shogonir/JavaSample/blob/master/src/util/MathUtil.java
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        static void Main(string[] args)
        {
            Console.WriteLine("三点がなす内角を求めるテスト");
            Console.WriteLine("angleDegree -> 内角");
            Point2 pOne = new Point2(1d, 0d);
            Point2 pZero = new Point2(0d, 0d);
            for (int angleDegree = 0; angleDegree <= 360; angleDegree += 10)
            {
                double angleRadian = angleDegree / 180f * Math.PI;
                
                // 単位円上の移動する点
                Point2 pMove = new Point2(Math.Cos(angleRadian), Math.Sin(angleRadian));
                MathUtil mu = new MathUtil();
                double internalAngleDegree = mu.CalculateInternalAngle(pOne, pZero, pMove);

                Console.WriteLine(string.Format("{0,3} -> {1:f3}", angleDegree, internalAngleDegree));
            }
        }
    }
    /// <summary>
    /// 二次元座標クラス
    /// </summary>
    public class Point2
    {
        private double X { get; set; }

        private double Y { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="X">X座標</param>
        /// <param name="Y">Y座標</param>
        public Point2(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        /// <summary>
        /// ベクトルの差を求める
        /// this.Point -> point
        /// </summary>
        /// <param name="point">引く座標</param>
        /// <returns></returns>
        public Point2 Substract(Point2 point)
        {
            return new Point2(this.X - point.X, this.Y - point.Y);
        }
        /// <summary>
        /// ベクトルの長さを求める
        /// </summary>
        /// <returns></returns>
        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        /// <summary>
        /// ベクトルの内積を求める
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double InnerProduct(Point2 point)
        {
            return this.X * point.X + this.Y * point.Y;
        }
        /// <summary>
        /// ベクトルの外積を求める
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double OuterProduct(Point2 point)
        {
            return this.X * point.Y - this.X * point.Y;
        }
    }
    /// <summary>
    /// 計算ライブラリ
    /// </summary>
    public class MathUtil
    {
        /// <summary>
        /// 三点がなす外角を求める
        /// </summary>
        /// <param name="p1">座標1</param>
        /// <param name="p2">座標2</param>
        /// <param name="p3">座標3</param>
        /// <returns>外角</returns>
        public double CalculateExternalAngle(Point2 p1, Point2 p2, Point2 p3)
        {
            Point2 vec1 = p2.Substract(p1);
            Point2 vec2 = p2.Substract(p3);
            double angleRadian = Math.Acos(vec1.InnerProduct(vec2) / (vec1.Magnitude() * vec2.Magnitude()));
            double angleDegree = angleRadian * 180 / Math.PI;
            if(vec1.OuterProduct(vec2) > 0)
            {
                return angleDegree - 180d;
            }
            else
            {
                return 180d - angleDegree;
            }
        }

        /// <summary>
        /// 三点がなす内角を求める
        /// </summary>
        /// <param name="p1">座標1</param>
        /// <param name="p2">座標2</param>
        /// <param name="p3">座標3</param>
        /// <returns>内角</returns>
        public double CalculateInternalAngle(Point2 p1, Point2 p2, Point2 p3)
        {
            double externalAngle = CalculateExternalAngle(p1, p2, p3);
            return 180d - externalAngle;
        }
    }
}
