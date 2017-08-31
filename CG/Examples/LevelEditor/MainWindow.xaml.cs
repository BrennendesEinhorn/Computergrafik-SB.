using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Shapes;
using DMS.Base;
using DMS.Geometry;
using LevelData;

namespace LevelEditor
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private Level levelData = new Level(); //todo: move to save method

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string[] args = Environment.GetCommandLineArgs();
			if(args.Length > 2)
			{
				if("autosave" == args[1].ToLower())
				{
					SaveLevelData(args[2]);
					Close();
				}
			}
		}

		private void SaveLevelData(string fileName)
		{
			//do not use autosize for canvas -> set a fixed size
			levelData.Bounds.SizeX = (float)canvas.ActualWidth;
			levelData.Bounds.SizeY = (float)canvas.ActualHeight;
			TraverseLogicalTree(canvas, string.Empty);
			levelData.ObjIntoBinFile(fileName);
		}

		private void TraverseLogicalTree(DependencyObject dependencyObject, string parentName) //todo: move to tools class
		{
			if (ReferenceEquals(null, dependencyObject)) return;
			var childern = LogicalTreeHelper.GetChildren(dependencyObject);
			foreach (var child in childern)
			{
				var type = child.GetType();
				if (typeof(Image) == type)
				{
					var image = child as Image;
					Convert(image, canvas, parentName);
				}
				else if (typeof(Ellipse) == type)
				{
					var collider = child as Ellipse;
					Convert(collider, parentName);
				}
				var logicalChild = child as FrameworkElement;
				TraverseLogicalTree(logicalChild, EditorTools.ResolveName(logicalChild.Name, parentName));
			}
		}

		private void Convert(Ellipse collider, string parentName)
		{
			var bounds = collider.ConvertBounds(canvas);
			var circle = CircleExtensions.CreateFromBox(bounds);
			levelData.Add(new ColliderCircle(EditorTools.ResolveName(collider.Name, parentName), circle));
		}

		private void Convert(Image image, Canvas canvas, string parentName)
		{
			var bounds = image.ConvertBounds(canvas);
			var layer = Canvas.GetZIndex(image);
			var sprite = new Sprite(EditorTools.ResolveName(image.Name, parentName), bounds, layer);
			sprite.TextureName = image.Source?.ToString();
			//todo1: register bitmap list
			sprite.Bitmap = image.Source.ToBitmap();
			levelData.Sprites.Add(sprite);
		}
	}
}
