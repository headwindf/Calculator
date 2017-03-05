using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Mainfrm : Form
    {
        private string calShow = "";//textBox显示的内容
        private double resultDouble = 0.0;//保存计算结果
        private bool equalStatus = false;//等于号是否被按过

        public Mainfrm()
        {
            InitializeComponent();
        }

        #region 窗体加载
        private void Mainfrm_Load(object sender, EventArgs e)
        {
            
            this.KeyPreview = true;//获取或设置一个值，该值指示在将键事件传递到具有焦点的控件前，窗体是否将接收此键事件
            btnNum0.BackgroundImage = new Bitmap(Resource1._0);
            btnNum1.BackgroundImage = new Bitmap(Resource1._1);
            btnNum2.BackgroundImage = new Bitmap(Resource1._2);
            btnNum3.BackgroundImage = new Bitmap(Resource1._3);
            btnNum4.BackgroundImage = new Bitmap(Resource1._4);
            btnNum5.BackgroundImage = new Bitmap(Resource1._5);
            btnNum6.BackgroundImage = new Bitmap(Resource1._6);
            btnNum7.BackgroundImage = new Bitmap(Resource1._7);
            btnNum8.BackgroundImage = new Bitmap(Resource1._8);
            btnNum9.BackgroundImage = new Bitmap(Resource1._9);
            btnAdd.BackgroundImage = new Bitmap(Resource1.add);
            btnDecrease.BackgroundImage = new Bitmap(Resource1.decrease);
            btnMul.BackgroundImage = new Bitmap(Resource1.mul);
            btnDiv.BackgroundImage = new Bitmap(Resource1.div);
            btnClear.BackgroundImage = new Bitmap(Resource1.clear);
            btnEqual.BackgroundImage = new Bitmap(Resource1.equal);
            btnDel.BackgroundImage = new Bitmap(Resource1.del);
            btnSquareRoot.BackgroundImage = new Bitmap(Resource1.squareRoot);
            btnPoint.BackgroundImage = new Bitmap(Resource1.point);
            btnPosAndNeg.BackgroundImage = new Bitmap(Resource1.posAndNeg);
            this.btnNum0.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum1.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum2.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum3.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum4.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum5.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum6.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum7.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum8.Click += new System.EventHandler(this.btnNum_Click);
            this.btnNum9.Click += new System.EventHandler(this.btnNum_Click);
            setBtnEnableMethon(false);//防止一开始点击运算符按钮导致出错
        }
        #endregion

        #region 数字按钮
        private void btnNum_Click(object sender, EventArgs e)
        {
            bool res = Regex.IsMatch(calShow, @"^0+$");//匹配全都是0的情况
            bool res1 = Regex.IsMatch(calShow, @"^(\-)?\d+(\.\d+)?(\+|\-|\*|\/)0+$");//匹配第二个操作数是否全都是0的情况
            if (res || res1)//满足其中一个则把前面的0删掉
            {
                btnDel.PerformClick();
            }
            if (equalStatus)//等于号按过之后点数字则直接清除原来的结果
            {
                equalStatus = false;
                calShow = "";
                resultDouble = 0.0;
                txtResual.Text = "";
            }
            Button btn = (Button)sender;
            calShow += btn.TabIndex.ToString();
            txtResual.Text = calShow;
            setBtnEnableMethon(true);
            btnEqual.Focus();
        }
        #endregion

        #region 小数点
        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (equalStatus)//等于号按过之后点数字则直接清除原来的结果
            {
                equalStatus = false;
                calShow = "";
                resultDouble = 0.0;
                txtResual.Text = "";
            }
            try
            {
                bool res = Regex.IsMatch(calShow, @"^(\-)?\d+$");//匹配纯数字
                bool res1 = Regex.IsMatch(calShow, @"^(\-)?\d+(\.\d+)?(\+|\-|\*|\/)\d+$");//匹配第二个操作数是否为整数
                if (res || res1)//满足其中一个则可以添加小数点
                {
                    calShow += ".";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("发生错误！", "警告");
            }
            txtResual.Text = calShow;
            setBtnEnableMethon(true);
        }
        #endregion

        #region 等于号
        private void btnEqual_Click(object sender, EventArgs e)
        {
            equalStatus = true;
            multiDeal(); 
        }
        #endregion

        #region 加法
        private void btnAdd_Click(object sender, EventArgs e)
        {
            equalStatus = false;
            multiDeal();
            calShow += "+";
            txtResual.Text = calShow;
            setBtnEnableMethon(false);
        }
        #endregion

        #region 减法
        private void btnDecrease_Click(object sender, EventArgs e)
        {
            equalStatus = false;
            multiDeal();
            calShow += "-";
            txtResual.Text = calShow;
            setBtnEnableMethon(false);
        }
        #endregion

        #region 乘法
        private void btnMul_Click(object sender, EventArgs e)
        {
            equalStatus = false;
            multiDeal();
            calShow += "*";
            txtResual.Text = calShow;
            setBtnEnableMethon(false);
        }
        #endregion

        #region 除法
        private void btnDiv_Click(object sender, EventArgs e)
        {
            equalStatus = false;
            multiDeal();
            calShow += "/";
            txtResual.Text = calShow;
            setBtnEnableMethon(false);
        }
        #endregion

        #region 运算符处理函数
        private void multiDeal()
        {
            try
            {
                //将前面的表达式的值计算出来
                Regex reg = new Regex(@"(\+|\-)?(\d+)(\.\d+)?(\+|\-|\*|\/)(\+|\-)?(\d+)(\.\d+)?");
                var res = reg.Match(calShow).Groups;
                //res[0]    整个匹配的表达式
                //res[1]    第一个操作数的符号
                //res[2]    第一个操作数的整数部分
                //res[3]    第一个操作数的小数部分
                //res[4]    操作符
                //res[5]    第二个操作数的符号
                //res[6]    第二个操作数的整数部分
                //res[7]    第而个操作数的小数部分
                switch (res[4].ToString())
                {
                    case "+":
                        resultDouble = Convert.ToDouble(res[1].ToString() + res[2].ToString() + res[3].ToString())
                                       + Convert.ToDouble(res[5].ToString() + res[6].ToString() + res[7].ToString());
                        calShow = resultDouble.ToString();
                        break;
                    case "-":
                        resultDouble = Convert.ToDouble(res[1].ToString() + res[2].ToString() + res[3].ToString())
                                       - Convert.ToDouble(res[5].ToString() + res[6].ToString() + res[7].ToString());
                        calShow = resultDouble.ToString();
                        break;
                    case "*":
                        resultDouble = Convert.ToDouble(res[1].ToString() + res[2].ToString() + res[3].ToString())
                                       * Convert.ToDouble(res[5].ToString() + res[6].ToString() + res[7].ToString());
                        calShow = resultDouble.ToString();
                        break;
                    case "/":
                        double secondNum = Convert.ToDouble(res[5].ToString() + res[6].ToString() + res[7].ToString());
                        if(secondNum == 0)
                            throw new Exception("除数不能为0！");
                        resultDouble = Convert.ToDouble(res[1].ToString() + res[2].ToString() + res[3].ToString())
                                       / secondNum;
                        calShow = resultDouble.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告");
            }      
            txtResual.Text = calShow;
        }
        #endregion

        #region 运算符按键使能与失能
        //设置为false是为了防止连续按运算符按钮发生错误
        private void setBtnEnableMethon(bool nowStatus)
        {
            btnAdd.Enabled = nowStatus;
            btnDecrease.Enabled = nowStatus;
            btnMul.Enabled = nowStatus;
            btnDiv.Enabled = nowStatus;
            btnEqual.Enabled = nowStatus;
            btnSquareRoot.Enabled = nowStatus;
            btnPosAndNeg.Enabled = nowStatus;
        }
        #endregion

        #region 清除键
        //清除所有
        private void btnClear_Click(object sender, EventArgs e)
        {
            calShow = "";
            resultDouble = 0;
            txtResual.Text = "";
        }
        #endregion

        #region textBox字体的大小自适应
        private void txtResual_TextChanged(object sender, EventArgs e)
        {
            if (calShow.Length < 10)
            {
                txtResual.Font = new Font(txtResual.Font.Name, 25);
            }
            else if (calShow.Length < 20)
            {
                txtResual.Font = new Font(txtResual.Font.Name, 15);
            }
            else if (calShow.Length < 30)
            {
                txtResual.Font = new Font(txtResual.Font.Name, 11);
            }
            else
            {
                txtResual.Font = new Font(txtResual.Font.Name, 8);
            }
        }
        #endregion

        #region 退格键
        private void btnDel_Click(object sender, EventArgs e)
        {
            if(calShow.Length != 0)
            {
                calShow = calShow.Substring(0,calShow.Length - 1);//删除最后一个字符
            }
            txtResual.Text = calShow;
            setBtnEnableMethon(true);
        }
        #endregion

        #region 开平方
        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            multiDeal();//先得出前面表达式的值
            try
            {
                Regex reg = new Regex(@"(\+|\-)?(\d+)(\.\d+)?");
                var res = reg.Match(calShow).Groups;

                if (res[1].ToString() == "-")
                {
                    throw new Exception("负数不能开根号！");
                }
                resultDouble = Math.Sqrt(Convert.ToDouble(calShow));
                calShow = resultDouble.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"警告");
            }
            txtResual.Text = calShow;
        }
        #endregion

        #region 正负号
        private void btnPosAndNeg_Click(object sender, EventArgs e)
        {
            multiDeal();//先计算结果，再来加正负号
            try
            {
                Regex reg = new Regex(@"^(\-)?");
                var res = reg.Match(calShow).Groups;
                if(res[1].ToString() == "")//正数
                {
                    calShow = "-" + calShow;
                }
                else//负数
                {
                    calShow = calShow.Substring(1,calShow.Length-1);
                }   
            }
            catch (Exception)
            {
                MessageBox.Show("发生错误！","警告");
            }
            txtResual.Text = calShow;
        }
        #endregion

        #region 按键输入
        private void Mainfrm_KeyDown(object sender, KeyEventArgs e)
        {

            switch(e.KeyCode)
            {
                case Keys.D0://字母上面的数字键
                    btnNum0.PerformClick();
                    break;
                case Keys.NumPad0://键盘右边的数字键
                    btnNum0.PerformClick();
                    break;
                case Keys.D1:
                    btnNum1.PerformClick();
                    break;
                case Keys.NumPad1:
                    btnNum1.PerformClick();
                    break;
                case Keys.D2:
                    btnNum2.PerformClick();
                    break;
                case Keys.NumPad2:
                    btnNum2.PerformClick();
                    break;
                case Keys.D3:
                    btnNum3.PerformClick();
                    break;
                case Keys.NumPad3:
                    btnNum3.PerformClick();
                    break;
                case Keys.D4:
                    btnNum4.PerformClick();
                    break;
                case Keys.NumPad4:
                    btnNum4.PerformClick();
                    break;
                case Keys.D5:
                    btnNum5.PerformClick();
                    break;
                case Keys.NumPad5:
                    btnNum5.PerformClick();
                    break;
                case Keys.D6:
                    btnNum6.PerformClick();
                    break;
                case Keys.NumPad6:
                    btnNum6.PerformClick();
                    break;
                case Keys.D7:
                    btnNum7.PerformClick();
                    break;
                case Keys.NumPad7:
                    btnNum7.PerformClick();
                    break;
                case Keys.D8:
                    btnNum8.PerformClick();
                    break;
                case Keys.NumPad8:
                    btnNum8.PerformClick();
                    break;
                case Keys.D9:
                    btnNum9.PerformClick();
                    break;
                case Keys.NumPad9:
                    btnNum9.PerformClick();
                    break;
                case Keys.Enter:
                    btnEqual.PerformClick();
                    break;
                case Keys.Add:
                    btnAdd.PerformClick();
                    break;
                case Keys.Subtract:
                    btnDecrease.PerformClick();
                    break;
                case Keys.Multiply:
                    btnMul.PerformClick();
                    break;
                case Keys.Divide:
                    btnDiv.PerformClick();
                    break;
                case Keys.Back:
                    btnDel.PerformClick();
                    break;
                case Keys.C:
                    btnClear.PerformClick();
                    break;
                case Keys.S:
                    btnSquareRoot.PerformClick();
                    break;
                case Keys.Z:
                    btnPosAndNeg.PerformClick();
                    break;
                case Keys.Decimal:
                    btnPoint.PerformClick();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
