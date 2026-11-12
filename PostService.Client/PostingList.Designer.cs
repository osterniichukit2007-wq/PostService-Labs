namespace PostService.Client
{
    partial class PostingList
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
            btnCreate = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            lblCount = new Label();
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
            gridPostings.Location = new Point(12, 60);
            gridPostings.MultiSelect = false;
            gridPostings.Name = "gridPostings";
            gridPostings.ReadOnly = true;
            gridPostings.RowHeadersVisible = false;
            gridPostings.RowHeadersWidth = 51;
            gridPostings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPostings.Size = new Size(1216, 445);
            gridPostings.TabIndex = 4;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 45F;
            colId.HeaderText = "№";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
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
            colDeliveryType.HeaderText = "Тип доставки";
            colDeliveryType.MinimumWidth = 6;
            colDeliveryType.Name = "colDeliveryType";
            colDeliveryType.ReadOnly = true;
            // 
            // colWeight
            // 
            colWeight.DataPropertyName = "Weight";
            colWeight.FillWeight = 70F;
            colWeight.HeaderText = "Вага, кг";
            colWeight.MinimumWidth = 6;
            colWeight.Name = "colWeight";
            colWeight.ReadOnly = true;
            // 
            // colWidth
            // 
            colWidth.DataPropertyName = "Width";
            colWidth.FillWeight = 70F;
            colWidth.HeaderText = "Ширина, см";
            colWidth.MinimumWidth = 6;
            colWidth.Name = "colWidth";
            colWidth.ReadOnly = true;
            // 
            // colHeight
            // 
            colHeight.DataPropertyName = "Height";
            colHeight.FillWeight = 70F;
            colHeight.HeaderText = "Висота, см";
            colHeight.MinimumWidth = 6;
            colHeight.Name = "colHeight";
            colHeight.ReadOnly = true;
            // 
            // colDepth
            // 
            colDepth.DataPropertyName = "Depth";
            colDepth.FillWeight = 70F;
            colDepth.HeaderText = "Глибина, см";
            colDepth.MinimumWidth = 6;
            colDepth.Name = "colDepth";
            colDepth.ReadOnly = true;
            // 
            // colValue
            // 
            colValue.DataPropertyName = "Value";
            colValue.FillWeight = 80F;
            colValue.HeaderText = "Цінність, грн";
            colValue.MinimumWidth = 6;
            colValue.Name = "colValue";
            colValue.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.FillWeight = 80F;
            colPrice.HeaderText = "Вартість, грн";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colCreatedAt
            // 
            colCreatedAt.DataPropertyName = "CreatedAt";
            colCreatedAt.FillWeight = 110F;
            colCreatedAt.HeaderText = "Створено";
            colCreatedAt.MinimumWidth = 6;
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.ReadOnly = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(12, 12);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(120, 36);
            btnCreate.TabIndex = 0;
            btnCreate.Text = "Створити";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(142, 12);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 36);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Редагувати";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(272, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 36);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Видалити";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(1108, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Оновити";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblCount
            // 
            lblCount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCount.AutoSize = true;
            lblCount.Location = new Point(12, 520);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(200, 20);
            lblCount.TabIndex = 5;
            lblCount.Text = "Усього відправлень: 0";
            // 
            // PostingList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1240, 560);
            Controls.Add(lblCount);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnCreate);
            Controls.Add(gridPostings);
            MinimumSize = new Size(900, 400);
            Name = "PostingList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Облік поштових відправлень";
            Load += PostingList_Load;
            ((System.ComponentModel.ISupportInitialize)gridPostings).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnCreate;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Label lblCount;
    }
}