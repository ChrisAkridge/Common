﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string[] lines = this.textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			List<double> values = new List<double>(lines.Length);

			foreach (string line in lines)
			{
				values.Add(double.Parse(line));
			}

			List<double> averages = new List<double>(values.Count);
			for (int i = 0; i < values.Count; i++)
			{
				double average = 0d;
				for (int j = 0; j < i; j++)
				{
					average += values[j];
				}
				average /= (i == 0) ? 1 : i;
				averages.Add(average);
			}

			this.chart1.Series[0].Points.Clear();
			StringBuilder builder = new StringBuilder();
			int k = 1;
			foreach (double average in averages)
			{
				this.chart1.Series[0].Points.Add(average);
				builder.Append(string.Format("{0}. ", k));
				builder.Append(average);
				builder.Append(Environment.NewLine);
				k++;
			}

			this.textBox1.Text = builder.ToString();

			double high = averages.OrderByDescending(a => a).First();
			double low = averages.OrderBy(a => a).First();
			
			double averagesAverage = 0d;
			averages.ForEach(a => averagesAverage += a);
			averagesAverage /= averages.Count;

			MessageBox.Show(string.Format("High: {0}{1}Low: {2}{1}Average: {3}", high, Environment.NewLine, low, averagesAverage));
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.chart1.Series[0].Points.Clear();
			for (int i = -128; i <= 127; i++)
			{
				int y = ~i;
				this.chart1.Series[0].Points.Add(y);
			}
		}
	}
}
