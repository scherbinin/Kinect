using System.Collections.Generic;

namespace BvhConverter.Stuff
{
    /// <summary>
    /// Служебные слова формата BHV
    /// </summary>
    public static class ConstsKeyWords
    {
        /// <summary>
        /// Самая первая строка, обьявляющая начало иерархии
        /// </summary>
        public const string Hierarchy = "HIERARCHY";
        /// <summary>
        /// Корень иерархии
        /// </summary>
        public const string Root = "ROOT ";
        /// <summary>
        /// {
        /// </summary>
        public const string LeftBrace = "{";
        /// <summary>
        /// }
        /// </summary>
        public const string RightBrace = "}";
        /// <summary>
        /// Служебное слово, после которого следуюет начальное положение координат
        /// </summary>
        public const string Offset = "OFFSET ";
        /// <summary>
        /// Таб
        /// </summary>
        public const string Tabulation = "\t";
        /// <summary>
        /// Количество параметров
        /// </summary>
        public const string ChannelsPozition = "CHANNELS 3 Xposition Yposition Zposition ";

        /// <summary>
        /// Количество параметров
        /// </summary>
        public const string ChannelsPozitionRotation = "CHANNELS 6 Xposition Yposition Zposition Zrotation Xrotation Yrotation";

        /// <summary>
        /// Количество параметров
        /// </summary>
        public const string ChannelsRotation = "CHANNELS 3 Zrotation Xrotation Yrotation";

        /// <summary>
        /// Очередное вхождение в иерархию
        /// </summary>
        public const string Joint = "JOINT ";

        /// <summary>
        /// Служебное слово, обозначающее вторую секцию в файле
        /// </summary>
        public const string Motion = "MOTION";
        /// <summary>
        /// Количество кадров
        /// </summary>
        public const string Frames = "Frames: ";
        /// <summary>
        /// Время на один кадр, используем всегда 30 кадров в секунду
        /// </summary>
        public const string FrameTime = "Frame Time: 0.033333";

        /// <summary>
        /// Последний элемент в Иерархии начинается со слова End
        /// </summary>
        public const string End = "End ";


    }

    /// <summary>
    /// Точки тела в формате BHV
    /// </summary>
    public static class ConstBodyPoints
    {
        /// <summary>
        /// Рутовая точка
        /// </summary>
        public const string Hips = "Hips";
        /// <summary>
        /// Левая часть рутового кресте, точка между левым бедром и точкой таза
        /// </summary>
        public const string LeftCross = "LeftCross";
        /// <summary>
        /// Левое бедро
        /// </summary>
        public const string LeftHip = "LeftHip";
        /// <summary>
        /// Правая часть рутового кресте, точка между правым бедром и точкой таза
        /// </summary>
        public const string RightCross = "RightCross";
        /// <summary>
        /// Правое бедро
        /// </summary>
        public const string RightHip = "RightHip";
        /// <summary>
        /// Правое колено
        /// </summary>
        public const string RightKnee = "RightKnee";
        /// <summary>
        /// Левое колено
        /// </summary>
        public const string LeftKnee = "LeftKnee";
        /// <summary>
        /// Проавая лодыжка
        /// </summary>
        public const string RightAnkle = "RightAnkle";
        /// <summary>
        /// Левая лодыжка
        /// </summary>
        public const string LeftAnkle = "LeftAnkle";
        /// <summary>
        ///  (Пальца рук, ног, макушка)
        /// </summary>
        public const string Site = "Site";


        /// <summary>
        /// Верхняя часть рутового креста, точка между Chest и точкой таза
        /// </summary>
        public const string UpCross = "UpCross";
        /// <summary>
        /// Поясница
        /// </summary>
        public const string Chest = "Chest";
        /// <summary>
        /// Левая ключица
        /// </summary>
        public const string LeftCollar = "LeftCollar";
        /// <summary>
        /// Правая ключица
        /// </summary>
        public const string RightCollar = "RightCollar";
        /// <summary>
        /// Левое плечо
        /// </summary>
        public const string LeftShoulder = "LeftShoulder";
        /// <summary>
        /// Правое плечо
        /// </summary>
        public const string RightShoulder = "RightShoulder";
        /// <summary>
        /// Левый локоть
        /// </summary>
        public const string LeftElbow = "LeftElbow";
        /// <summary>
        /// Правый локоть
        /// </summary>
        public const string RightElbow = "RightElbow";
        /// <summary>
        /// Левая Кисть
        /// </summary>
        public const string LeftWrist = "LeftWrist";
        /// <summary>
        /// Правая Кисть
        /// </summary>
        public const string RightWrist = "RightWrist";
        /// <summary>
        /// Начало шеи
        /// </summary>
        public const string Neck = "Neck";
        /// <summary>
        /// Средняя точка головы (уровень носа)
        /// </summary>
        public const string Head = "Head";

    }

    /// <summary>
    /// Определяет отношения один к одну для точек из кинекта к точкам из bvh
    /// </summary>
    public static class MepperTemplateToBvh
    {
        public static Dictionary<string, string> Container = new Dictionary<string, string>()
                                                                 {
                                                                     {" ", " "},
                                                                 };
    }

    /// <summary>
    /// тип BVH точки: 
    /// </summary>
    public enum BvhPointType
    {
        /// <summary>
        /// координаты позиции
        /// </summary>
        pozition,
        /// <summary>
        /// Повороты
        /// </summary>
        rotation,
        /// <summary>
        /// Обе составляющие
        /// </summary>
        Both
    }
}
