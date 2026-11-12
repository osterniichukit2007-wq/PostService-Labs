namespace PostService.Client
{
    partial class PostingEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblFromCaption = new Label();
            txtFrom = new TextBox();
            lblFromError = new Label();
            lblToCaption = new Label();
            txtTo = new TextBox();
            lblToError = new Label();
            lblContentCaption = new Label();
            txtContent = new TextBox();
            lblContentError = new Label();
            lblDeliveryCaption = new Label();
            cmbDeliveryType = new ComboBox();
            lblWeightCaption = new Label();
            txtWeight = new TextBox();
            lblWeightError = new Label();
            lblWidthCaption = new Label();
            txtWidth = new TextBox();
            lblWidthError = new Label();
            lblHeightCaption = new Label();
            txtHeight = new TextBox();
            lblHeightError = new Label();
            lblDepthCaption = new Label();
            txtDepth = new TextBox();
            lblDepthError = new Label();
            lblValueCaption = new Label();
            txtValue = new TextBox();
            lblValueError = new Label();
            lblPriceCaption = new Label();
            txtPrice = new TextBox();
            lblRequired = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(28, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Нове відправлення";
            // 
            // lblFromCaption
            // 
            lblFromCaption.AutoSize = true;
            lblFromCaption.Location = new Point(28, 69);
            lblFromCaption.Name = "lblFromCaption";
            lblFromCaption.TabIndex = 1;
            lblFromCaption.Text = "Відправник *";
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(230, 64);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(300, 27);
            txtFrom.TabIndex = 2;
            // 
            // lblFromError
            // 
            lblFromError.ForeColor = Color.FromArgb(179, 38, 30);
            lblFromError.Location = new Point(575, 68);
            lblFromError.Name = "lblFromError";
            lblFromError.Size = new Size(340, 20);
            lblFromError.TabIndex = 3;
            // 
            // lblToCaption
            // 
            lblToCaption.AutoSize = true;
            lblToCaption.Location = new Point(28, 109);
            lblToCaption.Name = "lblToCaption";
            lblToCaption.TabIndex = 4;
            lblToCaption.Text = "Отримувач *";
            // 
            // txtTo
            // 
            txtTo.Location = new Point(230, 104);
            txtTo.Name = "txtTo";
            txtTo.Size = new Size(300, 27);
            txtTo.TabIndex = 5;
            // 
            // lblToError
            // 
            lblToError.ForeColor = Color.FromArgb(179, 38, 30);
            lblToError.Location = new Point(575, 108);
            lblToError.Name = "lblToError";
            lblToError.Size = new Size(340, 20);
            lblToError.TabIndex = 6;
            // 
            // lblContentCaption
            // 
            lblContentCaption.AutoSize = true;
            lblContentCaption.Location = new Point(28, 149);
            lblContentCaption.Name = "lblContentCaption";
            lblContentCaption.TabIndex = 7;
            lblContentCaption.Text = "Вміст *";
            // 
            // txtContent
            // 
            txtContent.Location = new Point(230, 144);
            txtContent.Name = "txtContent";
            txtContent.Size = new Size(300, 27);
            txtContent.TabIndex = 8;
            // 
            // lblContentError
            // 
            lblContentError.ForeColor = Color.FromArgb(179, 38, 30);
            lblContentError.Location = new Point(575, 148);
            lblContentError.Name = "lblContentError";
            lblContentError.Size = new Size(340, 20);
            lblContentError.TabIndex = 9;
            // 
            // lblDeliveryCaption
            // 
            lblDeliveryCaption.AutoSize = true;
            lblDeliveryCaption.Location = new Point(28, 189);
            lblDeliveryCaption.Name = "lblDeliveryCaption";
            lblDeliveryCaption.TabIndex = 10;
            lblDeliveryCaption.Text = "Тип доставки *";
            // 
            // cmbDeliveryType
            // 
            cmbDeliveryType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDeliveryType.Location = new Point(230, 184);
            cmbDeliveryType.Name = "cmbDeliveryType";
            cmbDeliveryType.Size = new Size(300, 28);
            cmbDeliveryType.TabIndex = 11;
            // 
            // lblWeightCaption
            // 
            lblWeightCaption.AutoSize = true;
            lblWeightCaption.Location = new Point(28, 229);
            lblWeightCaption.Name = "lblWeightCaption";
            lblWeightCaption.TabIndex = 12;
            lblWeightCaption.Text = "Вага, кг *";
            // 
            // txtWeight
            // 
            txtWeight.Location = new Point(230, 224);
            txtWeight.Name = "txtWeight";
            txtWeight.Size = new Size(300, 27);
            txtWeight.TabIndex = 13;
            // 
            // lblWeightError
            // 
            lblWeightError.ForeColor = Color.FromArgb(179, 38, 30);
            lblWeightError.Location = new Point(575, 228);
            lblWeightError.Name = "lblWeightError";
            lblWeightError.Size = new Size(340, 20);
            lblWeightError.TabIndex = 14;
            // 
            // lblWidthCaption
            // 
            lblWidthCaption.AutoSize = true;
            lblWidthCaption.Location = new Point(28, 269);
            lblWidthCaption.Name = "lblWidthCaption";
            lblWidthCaption.TabIndex = 15;
            lblWidthCaption.Text = "Ширина, см *";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(230, 264);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(300, 27);
            txtWidth.TabIndex = 16;
            // 
            // lblWidthError
            // 
            lblWidthError.ForeColor = Color.FromArgb(179, 38, 30);
            lblWidthError.Location = new Point(575, 268);
            lblWidthError.Name = "lblWidthError";
            lblWidthError.Size = new Size(340, 20);
            lblWidthError.TabIndex = 17;
            // 
            // lblHeightCaption
            // 
            lblHeightCaption.AutoSize = true;
            lblHeightCaption.Location = new Point(28, 309);
            lblHeightCaption.Name = "lblHeightCaption";
            lblHeightCaption.TabIndex = 18;
            lblHeightCaption.Text = "Висота, см *";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(230, 304);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(300, 27);
            txtHeight.TabIndex = 19;
            // 
            // lblHeightError
            // 
            lblHeightError.ForeColor = Color.FromArgb(179, 38, 30);
            lblHeightError.Location = new Point(575, 308);
            lblHeightError.Name = "lblHeightError";
            lblHeightError.Size = new Size(340, 20);
            lblHeightError.TabIndex = 20;
            // 
            // lblDepthCaption
            // 
            lblDepthCaption.AutoSize = true;
            lblDepthCaption.Location = new Point(28, 349);
            lblDepthCaption.Name = "lblDepthCaption";
            lblDepthCaption.TabIndex = 21;
            lblDepthCaption.Text = "Глибина, см *";
            // 
            // txtDepth
            // 
            txtDepth.Location = new Point(230, 344);
            txtDepth.Name = "txtDepth";
            txtDepth.Size = new Size(300, 27);
            txtDepth.TabIndex = 22;
            // 
            // lblDepthError
            // 
            lblDepthError.ForeColor = Color.FromArgb(179, 38, 30);
            lblDepthError.Location = new Point(575, 348);
            lblDepthError.Name = "lblDepthError";
            lblDepthError.Size = new Size(340, 20);
            lblDepthError.TabIndex = 23;
            // 
            // lblValueCaption
            // 
            lblValueCaption.AutoSize = true;
            lblValueCaption.Location = new Point(28, 389);
            lblValueCaption.Name = "lblValueCaption";
            lblValueCaption.TabIndex = 24;
            lblValueCaption.Text = "Оголошена цінність, грн";
            // 
            // txtValue
            // 
            txtValue.Location = new Point(230, 384);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(300, 27);
            txtValue.TabIndex = 25;
            // 
            // lblValueError
            // 
            lblValueError.ForeColor = Color.FromArgb(179, 38, 30);
            lblValueError.Location = new Point(575, 388);
            lblValueError.Name = "lblValueError";
            lblValueError.Size = new Size(340, 20);
            lblValueError.TabIndex = 26;
            // 
            // lblPriceCaption
            // 
            lblPriceCaption.AutoSize = true;
            lblPriceCaption.Location = new Point(28, 429);
            lblPriceCaption.Name = "lblPriceCaption";
            lblPriceCaption.TabIndex = 27;
            lblPriceCaption.Text = "Вартість, грн";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(230, 424);
            txtPrice.Name = "txtPrice";
            txtPrice.ReadOnly = true;
            txtPrice.Size = new Size(300, 27);
            txtPrice.TabIndex = 28;
            txtPrice.TabStop = false;
            // 
            // lblRequired
            // 
            lblRequired.AutoSize = true;
            lblRequired.Location = new Point(28, 475);
            lblRequired.Name = "lblRequired";
            lblRequired.TabIndex = 29;
            lblRequired.Text = "* — обов'язкові поля";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(680, 535);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(110, 34);
            btnSave.TabIndex = 30;
            btnSave.Text = "Зберегти";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(800, 535);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 34);
            btnCancel.TabIndex = 31;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // PostingEdit
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(940, 590);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblRequired);
            Controls.Add(txtPrice);
            Controls.Add(lblPriceCaption);
            Controls.Add(lblValueError);
            Controls.Add(txtValue);
            Controls.Add(lblValueCaption);
            Controls.Add(lblDepthError);
            Controls.Add(txtDepth);
            Controls.Add(lblDepthCaption);
            Controls.Add(lblHeightError);
            Controls.Add(txtHeight);
            Controls.Add(lblHeightCaption);
            Controls.Add(lblWidthError);
            Controls.Add(txtWidth);
            Controls.Add(lblWidthCaption);
            Controls.Add(lblWeightError);
            Controls.Add(txtWeight);
            Controls.Add(lblWeightCaption);
            Controls.Add(cmbDeliveryType);
            Controls.Add(lblDeliveryCaption);
            Controls.Add(lblContentError);
            Controls.Add(txtContent);
            Controls.Add(lblContentCaption);
            Controls.Add(lblToError);
            Controls.Add(txtTo);
            Controls.Add(lblToCaption);
            Controls.Add(lblFromError);
            Controls.Add(txtFrom);
            Controls.Add(lblFromCaption);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PostingEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Нове відправлення";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblFromCaption;
        private TextBox txtFrom;
        private Label lblFromError;
        private Label lblToCaption;
        private TextBox txtTo;
        private Label lblToError;
        private Label lblContentCaption;
        private TextBox txtContent;
        private Label lblContentError;
        private Label lblDeliveryCaption;
        private ComboBox cmbDeliveryType;
        private Label lblWeightCaption;
        private TextBox txtWeight;
        private Label lblWeightError;
        private Label lblWidthCaption;
        private TextBox txtWidth;
        private Label lblWidthError;
        private Label lblHeightCaption;
        private TextBox txtHeight;
        private Label lblHeightError;
        private Label lblDepthCaption;
        private TextBox txtDepth;
        private Label lblDepthError;
        private Label lblValueCaption;
        private TextBox txtValue;
        private Label lblValueError;
        private Label lblPriceCaption;
        private TextBox txtPrice;
        private Label lblRequired;
        private Button btnSave;
        private Button btnCancel;
    }
}