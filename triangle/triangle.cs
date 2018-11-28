using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace triangle
{
    public partial class triangle : Form
    {

        #region khai báo 

        // Khai báo biến.
        private float[,] a = new float[10, 12];  // khai báo mảng chứa 10 thuộc tính: 3 góc, 3 cạnh, diện tích, nửa chu vi, bán kính đường tròn ngoại tiếp, chiều cao và 12 công thức tính của tam giác trong trường hợp bài toán này.
        private const double pi = 3.1415926535897931;   // Khai bao pi
        private const float totalTri = 180f;               // Tong 3 goc trong tam giac.

        public triangle()
        {
            InitializeComponent();
            init();
        }
        private void triangle_Load(object sender, EventArgs e)
        {
            cbValue.SelectedItem = cbValue.Items[0];
        }

        #endregion

        #region khởi tạo 

        private void init()
        {
            float set = -1;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 12; j++)
                    a[i, j] = 0;
            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = set; // cạnh a
            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = set;// cạnh b
            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = set;// cạnh c
            a[3, 0] = a[3, 2] = a[3, 5] = a[3, 8] = set;// góc Alpha
            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = set;// góc Beta
            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = set;// góc Deta
            a[6, 11] = set;// chiều cao ha
            a[7, 2] = a[7, 3] = a[7, 4] = a[7, 10] = set;// diên tích S
            a[8, 1] = set;// nửa chu vi p
            a[9, 10] = set;// bán kính đường tròn ngoại tiếp tam giác
        }

        #endregion

        #region xử lý 

        private bool FindSuccess()
        {
            switch (cbValue.SelectedIndex)
            {
                case 1:
                    if (a[3, 0] == -1) break;// góc Alpha
                    return true;
                case 2:
                    if (a[4, 0] == -1) break;// góc beta
                    return true;
                case 3:
                    if (a[5, 0] == -1) break;// góc Deta
                    return true;
                case 4:
                    if (a[0, 1] == -1) break;// Cạnh a
                    return true;
                case 5:
                    if (a[1, 1] == -1) break;// Cạnh b
                    return true;
                case 6:
                    if (a[2, 1] == -1) break;// Cạnh C
                    return true;
                case 7:
                    if (a[7, 2] == -1) break;// Diện tích S
                    return true;
                case 8:
                    if (a[8, 1] == -1) break;// nửa chu vi p
                    return true;
                case 9:
                    if (a[9, 10] == -1) break;// bán kính R
                    return true;
                case 10:
                    if (a[6, 1] == -1) break;// Chiều cao ha
                    return true;
            }
            return false;
        }

        //giải phương trình bậc hai  ax2 + bx + c = 0

        private double bac2(double a,double b,double c)
        {
            double value = -1;
            double x1 = 0, x2 = 0;
            double denta = 0;
            denta = (b * b - 4 * a * c);
            if (denta > 0)
            {
                x1 = (-b - Math.Sqrt(denta)) / (2 * a);
                x2 = (-b + Math.Sqrt(denta)) / (2 * a);
            }
            else if (denta == 0)
                x1 = x2 = (-b / (2 * a));

            if (x1 > 0)
                value = x1;
            if (x2 > 0)
                value = x2;
            return value;
        }

        // Lấy vị trí yếu tố chưa biết.
        private int getNotKnow(int k)
        {

            int counter = 0;
            int number = -1;
            for (int i = 0; i < 10; i++)
            {
                if (a[i, k] == -1)
                {
                    counter++;
                    number = i;
                }
            }
            if (counter == 1)
            {
                return number;
            }
            return -1;
        }

        // Kích hoạt theo cơ chế lan truyền.
        private void activeSpread(int j, int notKnow)
        {
            float value = -1;
            switch (j)
            {
                case 0: //A + B + C  =  180
                    switch (notKnow)
                    {
                        case 3:
                            value = (float)(totalTri - a[4, 0] - a[5, 0]);
                            a[3, 0] = a[3, 2] = a[3, 5] = a[3, 8] = value;
                            txA.Text = a[3, 0].ToString();
                            break;
                        case 4:
                            value = (float)(totalTri - a[3, 0] - a[5, 0]);
                            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = value;
                            txB.Text = a[4, 0].ToString();
                            break;
                        case 5:
                            value = (float)(totalTri - a[3, 0] - a[4, 0]);
                            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = value;
                            txC.Text = a[5, 0].ToString();
                            break;
                    }
                    break;
                case 1: // 2p = a + b + c
                    switch (notKnow)
                    {
                        case 0:
                            value = (float)(2 * a[8, 1] - a[1, 1] - a[2, 1]);
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)(2 * a[8, 1] - a[0, 1] - a[2, 1]);
                            a[1, 1] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 2:
                            value = (float)(2 * a[8, 1] - a[0, 1] - a[1, 1]);
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 8:
                            value = (float)((a[0, 1] + a[1, 1] + a[2, 1]) / 2);
                            a[8, 1] = value;
                            txp.Text = value.ToString();
                            break;
                    }
                    break;
                case 2: //công thức S=b.c.sinA /2
                    switch (notKnow)
                    {
                        case 1:
                            value = (float)(a[7, 2] / (a[2, 2] * Math.Sin((double)(a[3, 2] * pi / totalTri))));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 2:
                            value = (float)(a[7, 2] / (a[1, 2] * Math.Sin((double)(a[3, 2] * pi / totalTri))));
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 3:
                            value = (float)((Math.Asin((double)(2 * a[7, 2] / (a[1, 2] * a[2, 2])))) * totalTri / pi);
                            a[3, 0] = a[3, 2] = a[3, 5] = a[3, 8] = value;
                            txA.Text = value.ToString();
                            break;
                        case 7:
                            value = (float)(a[1, 2] * a[2, 2] * Math.Sin((double)(a[3, 2] * pi / totalTri)) * 0.5f);
                            a[7, 2] = a[7, 3] = a[7, 4] = a[7, 10] = value;
                            txS.Text = value.ToString();
                            break;
                    }
                    break;
                case 3: // S=c.a.sinB /2
                    switch (notKnow)
                    {
                        case 0:
                            value = (float)(a[7, 3] / (a[2, 3] * Math.Sin((double)(a[4, 3] * pi / totalTri))));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 2:
                            value = (float)(a[7, 3] / (a[0, 3] * Math.Sin((double)(a[4, 3] * pi / totalTri)))); ;
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 4:
                            value = (float)((Math.Asin((double)(2 * a[7, 3] / (a[0, 3] * a[2, 3])))) * totalTri / pi);
                            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = value;
                            txB.Text = value.ToString();
                            break;
                        case 7:
                            value = (float)((a[2, 3] * a[0, 3] * Math.Sin((double)(a[4, 3] * pi / totalTri))) / 2);
                            a[7, 2] = a[7, 3] = a[7, 4] = a[7, 10] = value;
                            txS.Text = value.ToString();
                            break;
                    }
                    break;
                case 4: //S=a.b.sinC/2
                    switch (notKnow)
                    {
                        case 0:
                            value = (float)(a[7, 4] / (a[1, 4] * Math.Sin((double)(a[5, 4] * pi / totalTri))));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)(a[7, 4] / (a[0, 4] * Math.Sin((double)(a[5, 4] * pi / totalTri))));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 5:
                            value = (float)((Math.Asin((double)(2 * a[7, 4] / (a[0, 4] * a[1, 4])))) * totalTri / pi);
                            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = value;
                            txC.Text = value.ToString();
                            break;
                        case 7:
                            value = (float)((a[1, 4] * a[0, 4] * Math.Sin((double)(a[5, 4] * pi / totalTri))) / 2);
                            a[7, 2] = a[7, 3] = a[7, 4] = a[7, 10] = value;
                            txS.Text = value.ToString();
                            break;
                    }
                    break;
                case 5: //a2 = b2 + c2 - 2.b.c.cosA
                    switch (notKnow)
                    {
                        case 0:
                            value = (float)Math.Sqrt((double)(a[1, 5] * a[1, 5] + a[2, 5] * a[2, 5] - 2 * a[1, 5] * a[2, 5] * (float)Math.Cos((double)a[3, 5] * pi / totalTri)));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)bac2(1, (double)(-2 * a[2, 5] * Math.Cos((double)(a[3, 5] * pi / totalTri))), (double)(a[2, 5] * a[2, 5] - (a[0, 5] * a[0, 5])));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 2:
                            value = (float)bac2(1, (double)(-2 * a[1, 5] * Math.Cos((double)(a[3, 5] * pi / totalTri))), (double)(a[1, 5] * a[1, 5] - (a[0, 5] * a[0, 5])));
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 3:
                            value = (float)(Math.Acos((double)((a[1, 5] * a[1, 5] + a[2, 5] * a[2, 5] - a[0, 5] * a[0, 5]) / (2 * a[1, 5] * a[2, 5]))) * totalTri / pi);
                            a[3, 0] = a[3, 2] = a[3, 5] = a[3, 8] = value;
                            txA.Text = value.ToString();
                            break;
                    }
                    break;
                case 6: //b2 = a2 + c2 -2.a.c.cosB
                    switch (notKnow)
                    {
                        case 1:
                            value = (float)Math.Sqrt((double)(a[0, 6] * a[0, 6] + a[2, 6] * a[2, 6] - 2 * a[0, 6] * a[2, 6] * (float)Math.Cos((double)a[4, 6] * pi / totalTri)));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 0:
                            value = (float)bac2(1, (double)(-2 * a[2, 6] * Math.Cos((double)(a[4, 6] * pi / totalTri))), (double)(a[2, 6] * a[2, 6] - (a[1, 6] * a[1, 6])));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 2:
                            value = (float)bac2(1, (double)(-2 * a[0, 6] * Math.Cos((double)(a[4, 6] * pi / totalTri))), (double)(a[0, 6] * a[0, 6] - (a[1, 6] * a[1, 6])));
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 4:
                            value = (float)(Math.Acos((double)((a[0, 6] * a[0, 6] + a[2, 6] * a[2, 6] - a[1, 6] * a[1, 6]) / (2 * a[0, 6] * a[2, 6]))) * totalTri / pi);
                            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = value;
                            txB.Text = value.ToString();
                            break;
                    }
                    break;
                case 7: // c2 = a2 + b2 - 2.a.b.cosC
                    switch (notKnow)
                    {
                        case 2:
                            value = (float)Math.Sqrt((double)(a[0, 7] * a[0, 7] + a[1, 7] * a[1, 7] - 2 * a[0, 7] * a[1, 7] * (float)Math.Cos((double)a[5, 7] * pi / totalTri)));
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 0:
                            value = (float)bac2(1, (double)(-2 * a[1, 7] * Math.Cos((double)(a[5, 7] * pi / totalTri))), (double)(a[0, 7] * a[0, 7] - (a[2, 7] * a[2, 7])));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)bac2(1, (double)(-2 * a[0, 7] * Math.Cos((double)(a[5, 7] * pi / totalTri))), (double)(a[1, 7] * a[1, 7] - (a[2, 7] * a[2, 7])));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 5:
                            value = (float)(Math.Acos((double)((a[0, 7] * a[0, 7] + a[1, 7] * a[1, 7] - a[2, 7] * a[2, 7]) / (2 * a[0, 7] * a[1, 7]))) * totalTri / pi);
                            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = value;
                            txC.Text = value.ToString();
                            break;
                    }
                    break;
                case 8: //a sin B= b sin A
                    switch (notKnow)
                    {
                        case 0:
                            value = (float)((a[1, 8] * Math.Sin((double)(a[3, 8] * pi / totalTri))) / Math.Sin((double)(a[4, 8] * pi / totalTri)));
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)((a[0, 8] * Math.Sin((double)(a[4, 8] * pi / totalTri))) / Math.Sin((double)(a[3, 8] * pi / totalTri)));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 3:
                            value = (float)(Math.Asin((double)((a[0, 8] * Math.Sin((double)(a[4, 8] * pi / totalTri))) / a[1, 8])) * totalTri / pi);
                            a[3, 0] = a[3, 2] = a[3, 5] = a[3, 8] = value;
                            txA.Text = value.ToString();
                            break;
                        case 4:
                            value = (float)(Math.Asin((double)((a[1, 8] * Math.Sin((double)(a[3, 8] * pi / totalTri))) / a[0, 8])) * totalTri / pi);
                            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = value;
                            txB.Text = value.ToString();
                            break;
                    }
                    break;
                case 9: // b.sinC = c.sinB
                    switch (notKnow)
                    {
                        case 2:
                            value = (float)(a[1, 9] * Math.Sin((double)(a[5, 9] * pi / totalTri)) / Math.Sin((double)(a[4, 9] * pi / totalTri)));
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 1:
                            value = (float)((a[2, 9] * Math.Sin((double)(a[4, 9] * pi / totalTri))) / Math.Sin((double)(a[5, 9] * pi / totalTri)));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 5:
                            value = (float)(Math.Asin((double)((a[2, 9] * Math.Sin((double)(a[4, 9] * pi / totalTri))) / a[1, 9])) * totalTri / pi);
                            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = value;
                            txC.Text = value.ToString();
                            break;
                        case 4:
                            value = (float)(Math.Asin((double)((a[1, 9] * Math.Sin((double)(a[5, 9] * pi / totalTri))) / a[2, 9])) * totalTri / pi);
                            a[4, 0] = a[4, 3] = a[4, 6] = a[4, 8] = a[4, 9] = value;
                            txB.Text = value.ToString();
                            break;
                    }
                    break;
                case 10: // R =  abc/4S
                    switch (notKnow)
                    {
                        case 0:
                            value = (a[9, 10] * 4 * a[7, 10]) / (a[1, 10] * a[2, 10]);
                            a[0, 1] = a[0, 3] = a[0, 4] = a[0, 5] = a[0, 6] = a[0, 7] = a[0, 8] = a[0, 10] = value;
                            txCa.Text = value.ToString();
                            break;
                        case 1:
                            value = (a[9, 10] * 4 * a[7, 10]) / (a[0, 10] * a[2, 10]);
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 2:
                            value = (a[9, 10] * 4 * a[7, 10]) / (a[1, 10] * a[0, 10]);
                            a[2, 1] = a[2, 2] = a[2, 3] = a[2, 5] = a[2, 6] = a[2, 7] = a[2, 9] = a[2, 10] = value;
                            txCc.Text = value.ToString();
                            break;
                        case 7:
                            value = (a[0, 10] * a[1, 10] * a[2, 10]) / (4 * a[9, 10]);
                            a[7, 2] = a[7, 3] = a[7, 4] = a[7, 10] = value;
                            txS.Text = value.ToString();
                            break;
                        case 9:
                            value = (a[0, 10] * a[1, 10] * a[2, 10]) / (4 * a[7, 10]);
                            a[9, 10] = value;
                            txR.Text = value.ToString();
                            break;
                    }
                    break;
                case 11: //ha= b sin C
                    switch (notKnow)
                    {
                        case 1:
                            value = (float)(a[6, 11] / Math.Sin((double)(a[5, 11] * pi / totalTri)));
                            a[1, 1] = a[1, 2] = a[1, 4] = a[1, 5] = a[1, 6] = a[1, 7] = a[1, 8] = a[1, 9] = a[1, 10] = a[1, 11] = value;
                            txCb.Text = value.ToString();
                            break;
                        case 5:
                            value = (float)(Math.Asin((double)(a[6, 11] / a[1, 11])) * totalTri / pi);
                            a[5, 0] = a[5, 4] = a[5, 7] = a[5, 9] = a[5, 11] = value;
                            txC.Text = value.ToString();
                            break;
                        case 6:
                            value = (float)(a[1, 11] * Math.Sin((double)(a[5, 11] * pi / totalTri)));
                            a[6, 11] = value;
                            txha.Text = value.ToString();
                            break;
                    }
                    break;
            }

        }

        // Kích hoạt yếu tố chưa biết.
        private void activeNotKnow()
        {
            int temp;
            if (!string.IsNullOrEmpty(txCa.Text))
            {
                float num = float.Parse(txCa.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[0, temp] == -1f)
                        a[0, temp] = num;
                }
            }
            if (!string.IsNullOrEmpty(txCb.Text))
            {
                float num1 = float.Parse(txCb.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[1, temp] == -1f)
                        a[1, temp] = num1;
                }
            }
            if (!string.IsNullOrEmpty(txCc.Text))
            {
                float num2 = float.Parse(txCc.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[2, temp] == -1f)
                        a[2, temp] = num2;
                }
            }
            if (!string.IsNullOrEmpty(txA.Text))
            {
                float num3 = float.Parse(txA.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[3, temp] == -1f)
                        a[3, temp] = num3;
                }
            }
            if (!string.IsNullOrEmpty(txB.Text))
            {
                float num4 = float.Parse(txB.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[4, temp] == -1f)
                        a[4, temp] = num4;
                }
            }
            if (!string.IsNullOrEmpty(txC.Text))
            {
                float num5 = float.Parse(txC.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[5, temp] == -1f)
                        a[5, temp] = num5;
                }
            }
            if (!string.IsNullOrEmpty(txha.Text))
            {
                float num6 = float.Parse(txha.Text);
                if (a[6, 11] == -1f)
                    a[6, 11] = num6;
            }
            if (!string.IsNullOrEmpty(txS.Text))
            {
                float num7 = float.Parse(txS.Text);
                for (temp = 0; temp < 12; temp++)
                {
                    if (a[7, temp] == -1f)
                        a[7, temp] = num7;
                }
            }
            if (!string.IsNullOrEmpty(txp.Text))
            {
                float num8 = float.Parse(txp.Text);
                if (a[8, 1] == -1f)
                    a[8, 1] = num8;
            }
            if (!string.IsNullOrEmpty(txR.Text))
            {
                float num9 = float.Parse(txR.Text);
                if (a[9, 10] == -1f)
                    a[9, 10] = num9;
            }
        }

        private void Solution()
        {
            activeNotKnow();
            bool flag = true;
            while (flag)
            {
                //activeNotKnow();
                flag = false;
                for (int i = 0; i < 12; i++)
                {
                    int notKnow = getNotKnow(i);
                    if (notKnow != -1)
                    {
                        activeSpread(i, notKnow);
                        flag = true;
                        if (FindSuccess())
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region button event

        private void btStart_Click(object sender, EventArgs e)
        {
            if (cbValue.SelectedItem == cbValue.Items[0])
            {
                MessageBox.Show("Bạn Chưa chọn Giá trị cần tính. \n Vui lòng chọn một giá trị cần tính rồi mới nhấn Bắt Đầu tính!", "Cảnh báo từ chương trình!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                Solution();
            }
        }

        private void btRestart_Click(object sender, EventArgs e)
        {
            txA.Text = txB.Text = txC.Text = string.Empty;
            txCa.Text = txCb.Text = txCc.Text = string.Empty;
            txS.Text = txR.Text = txp.Text = txha.Text = string.Empty;
            init();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
