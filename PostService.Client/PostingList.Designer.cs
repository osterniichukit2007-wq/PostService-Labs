namespace PostService.Client
{
    partial class PostingList
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gridPostings = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colFrom = new DataGridViewTextBoxColumn();
            colTo = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            colDeliveryType = new DataGridViewTextBoxColumn();
            colWeight = new DataGridViewTextBoxColumn();
            colWidth = new DataGridViewTextBoxColumn();
            colHeight = new DataGridViewTextBoxColumn();
            colDepth = new DataGridViewTextBoxColumn();
            colValue = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();
            btnDelete = new Button();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)gridPostings).BeginInit();
            SuspendLayout();
            // 
            // gridPostings
            // 
            gridPostings.AllowUserToAddRows = false;
            gridPostings.AllowUserToDeleteRows = false;
            gridPostings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPostings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPostings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPostings.Columns.AddRange(new DataGridViewColumn[] { colId, colFrom, colTo, colContent, colDeliveryType, colWeight, colWidth, colHeight, colDepth, colValue, colPrice, colCreatedAt });
            gridPostings.Location = new Point(12, 109);
            gridPostings.MultiSelect = false;
            gridPostings.Name = "gridPostings";
            gridPostings.ReadOnly = true;
            gridPostings.RowHeadersWidth = 51;
            gridPostings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPostings.Size = new Size(1302, 242);
            gridPostings.TabIndex = 0;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "Id";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colFrom
            // 
            colFrom.DataPropertyName = "From";
            colFrom.HeaderText = "Відправник";
            colFrom.MinimumWidth = 6;
            colFrom.Name = "colFrom";
            colFrom.ReadOnly = true;
            // 
            // colTo
            // 
            colTo.DataPropertyName = "To";
            colTo.HeaderText = "Отримувач";
            colTo.MinimumWidth = 6;
            colTo.Name = "colTo";
            colTo.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "Content";
            colContent.HeaderText = "Вміст";
            colContent.MinimumWidth = 6;
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // colDeliveryType
            // 
            colDeliveryType.DataPropertyName = "DeliveryType";
            colDeliveryType.HeaderText = "Спосіб доставки";
            colDeliveryType.MinimumWidth = 6;
            colDeliveryType.Name = "colDeliveryType";
            colDeliveryType.ReadOnly = true;
            // 
            // colWeight
            // 
            colWeight.DataPropertyName = "Weight";
            colWeight.HeaderText = "Вага";
            colWeight.MinimumWidth = 6;
            colWeight.Name = "colWeight";
            colWeight.ReadOnly = true;
            // 
            // colWidth
            // 
            colWidth.DataPropertyName = "Width";
            colWidth.HeaderText = "Ширина";
            colWidth.MinimumWidth = 6;
            colWidth.Name = "colWidth";
            colWidth.ReadOnly = true;
            // 
            // colHeight
            // 
            colHeight.DataPropertyName = "Height";
            colHeight.HeaderText = "Висота";
            colHeight.MinimumWidth = 6;
            colHeight.Name = "colHeight";
            colHeight.ReadOnly = true;
            // 
            // colDepth
            // 
            colDepth.DataPropertyName = "Depth";
            colDepth.HeaderText = "Глибина";
            colDepth.MinimumWidth = 6;
            colDepth.Name = "colDepth";
            colDepth.ReadOnly = true;
            // 
            // colValue
            // 
            colValue.DataPropertyName = "Value";
            colValue.HeaderText = "Цінність";
            colValue.MinimumWidth = 6;
            colValue.Name = "colValue";
            colValue.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.HeaderText = "Вартість";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colCreatedAt
            // 
            colCreatedAt.DataPropertyName = "CreatedAt";
            colCreatedAt.HeaderText = "Створено";
            colCreatedAt.MinimumWidth = 6;
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.ReadOnly = true;
            colCreatedAt.Visible = false;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(476, 357);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Видалити";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(757, 357);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Оновити";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // PostingList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1363, 530);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(gridPostings);
            Name = "PostingList";
            Text = "Поштові відправлення";
            Load += PostingList_Load;
            ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView gridPostings;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colFrom;
        private DataGridViewTextBoxColumn colTo;
        private DataGridViewTextBoxColumn colContent;
        private DataGridViewTextBoxColumn colDeliveryType;
        private DataGridViewTextBoxColumn colWeight;
        private DataGridViewTextBoxColumn colWidth;
        private DataGridViewTextBoxColumn colHeight;
        private DataGridViewTextBoxColumn colDepth;
        private DataGridViewTextBoxColumn colValue;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colCreatedAt;
        private Button btnDelete;
        private Button btnRefresh;
    }
}
