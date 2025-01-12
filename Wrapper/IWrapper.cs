namespace WrapperLib
{
	/// <summary>
	/// Интерфейс, реализующий базовое взаимодействие с API САПР
	/// </summary>
	public interface IWrapper
	{
		/// <summary>
		/// Открыть САПР
		/// </summary>
		void OpenCad();

		/// <summary>
		/// Создать деталь
		/// </summary>
		void CreatePart();

        /// <summary>
        /// Создать эскиз с прямоугольником
        /// </summary>
        /// <param name="x">Координата X начала</param>
        /// <param name="y">Координата Y начала</param>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <param name="name">Имя эскиза</param>
        void NewRectangle(double x, double y, int width, int height, string name);

		/// <summary>
		/// Выдавить последний созданный эскиз по оси OZ
		/// </summary>
		/// <param name="height">Глубина выдавливания</param>
		/// <param name="name">Имя результирующего тела</param>
		/// <param name="positiveDirection">Направление выдавливания - в положительную сторону оси</param>
		void Extrude(int height, string name, bool positiveDirection);

		/// <summary>
		/// Открыта ли САПР
		/// </summary>
		/// <returns></returns>
		bool IsCadRunning();
    }
}
