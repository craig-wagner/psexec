#region Using...
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
#endregion

namespace TestingPsExec
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
  public class Form1 : System.Windows.Forms.Form {
    #region private variables and controls
    private System.Windows.Forms.Button button1;

    private Process myCmd;
    private System.Windows.Forms.TextBox cmdOutPut;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtTarget;
    private System.Windows.Forms.TextBox txtUser;
    private System.Windows.Forms.TextBox txtPass;
    #endregion

    public Form1() {
      InitializeComponent();

      myCmd = new Process();
      myCmd.Exited += new System.EventHandler(myCmdProcess_exited);
    }

    #region Designer code
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;


    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing ) {
      if( disposing ) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.button1 = new System.Windows.Forms.Button();
      this.cmdOutPut = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtTarget = new System.Windows.Forms.TextBox();
      this.txtUser = new System.Windows.Forms.TextBox();
      this.txtPass = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(80, 88);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(120, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "GO";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // cmdOutPut
      // 
      this.cmdOutPut.Location = new System.Drawing.Point(0, 120);
      this.cmdOutPut.Multiline = true;
      this.cmdOutPut.Name = "cmdOutPut";
      this.cmdOutPut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.cmdOutPut.Size = new System.Drawing.Size(288, 152);
      this.cmdOutPut.TabIndex = 2;
      this.cmdOutPut.Text = "";
      this.cmdOutPut.WordWrap = false;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(0, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 23);
      this.label1.TabIndex = 3;
      this.label1.Text = "Target";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(0, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 23);
      this.label2.TabIndex = 4;
      this.label2.Text = "User";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(0, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(48, 23);
      this.label3.TabIndex = 5;
      this.label3.Text = "Pass";
      // 
      // txtTarget
      // 
      this.txtTarget.Location = new System.Drawing.Point(48, 5);
      this.txtTarget.Name = "txtTarget";
      this.txtTarget.Size = new System.Drawing.Size(240, 20);
      this.txtTarget.TabIndex = 6;
      this.txtTarget.Text = "\\\\grd-dev";
      // 
      // txtUser
      // 
      this.txtUser.Location = new System.Drawing.Point(48, 29);
      this.txtUser.Name = "txtUser";
      this.txtUser.Size = new System.Drawing.Size(240, 20);
      this.txtUser.TabIndex = 7;
      this.txtUser.Text = "TestCsRemoteExec";
      // 
      // txtPass
      // 
      this.txtPass.Location = new System.Drawing.Point(48, 54);
      this.txtPass.Name = "txtPass";
      this.txtPass.Size = new System.Drawing.Size(240, 20);
      this.txtPass.TabIndex = 8;
      this.txtPass.Text = "northwest";
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(292, 278);
      this.Controls.Add(this.txtPass);
      this.Controls.Add(this.txtUser);
      this.Controls.Add(this.txtTarget);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cmdOutPut);
      this.Controls.Add(this.button1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
      Application.Run(new Form1());
    }

    #endregion

    private void button1_Click(object sender, System.EventArgs e) {      
      string args = txtTarget.Text + " -u " + txtUser.Text + " -p " + txtPass.Text + @" ksh.bat C:\AI_Sandbox\Dev\TavisJohnson\ItemResponseKeyChecker\run\WRAPPER_tempgraph1.bat > output.txt";
      
      myCmd.StartInfo.FileName = "psexec.exe";
      myCmd.StartInfo.Arguments = args;
      myCmd.StartInfo.RedirectStandardOutput = true;
      myCmd.StartInfo.UseShellExecute = false;
      myCmd.StartInfo.CreateNoWindow = true;
      myCmd.EnableRaisingEvents = true;

      cmdOutPut.Text = "";
      try {
        cmdOutPut.Text = "Starting remote process... \n";
        cmdOutPut.Update();

        myCmd.Start();
      } catch (Exception ex) {
        cmdOutPut.Text = ex.Message;
      }
    }
	
    private void myCmdProcess_exited(object sender, System.EventArgs e) {
      try {
        System.IO.StreamReader myFile =	new System.IO.StreamReader("output.txt");
        string myString = myFile.ReadToEnd();
        myFile.Close();
        cmdOutPut.AppendText(myString);
      }
      catch(Exception ex) {
        cmdOutPut.Text = ex.Message; 
      }

    }
  }
}
