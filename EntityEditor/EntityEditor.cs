using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BFECore.EntitySystem;

namespace EntityEditor
{
	public partial class EntityEditor : Form
	{
		private BaseEntitySystem m_ES;
		private Entity selectedEntity = null;

		public EntityEditor()
		{
			InitializeComponent();

			m_ES = new BaseEntitySystem();
			UpdateList();

			ddlComponents.Items.AddRange(BaseComponent.ComponentTypes.ToArray());
			ddlAssemblages.Items.AddRange(Assemblages.AssemblageInstructions.Keys.ToArray());
		}

        public EntityEditor(BaseEntitySystem bes)
        {
            InitializeComponent();

            m_ES = bes;
            UpdateList();

            ddlComponents.Items.AddRange(BaseComponent.ComponentTypes.ToArray());
            ddlAssemblages.Items.AddRange(Assemblages.AssemblageInstructions.Keys.ToArray());
        }

		private void btnLoad_Click(object sender, EventArgs e)
		{
			try
			{
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.Cursor = Cursors.WaitCursor;

					m_ES.Load(openFileDialog1.FileName);
					selectedEntity = null;
					UpdateList();
				}
			}
			finally
			{
				this.Cursor = Cursors.Default; 
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.Cursor = Cursors.WaitCursor;
					m_ES.Save(saveFileDialog1.FileName);
				}
			}
			finally
			{
				this.Cursor = Cursors.Default; 
			}
		}

		private void btnAddEntity_Click(object sender, EventArgs e)
		{
			selectedEntity = new Entity(m_ES.CreateEntityID());						
			UpdateList();

			txtEntityName.Focus();
		}

		private void btnDeleteEntity_Click(object sender, EventArgs e)
		{
			if (lstEntityList.SelectedItem != null)
			{
				m_ES.KillEntity((Entity)lstEntityList.SelectedItem);
				selectedEntity = null;

				UpdateList();

				tabComponents.TabPages.Clear();
			}
		}

		private void UpdateList()
		{
            if (m_ES.EditType == EditorType.EntityMode)
            {
                rbEntity.Checked = true;
            }
            else
            {
                rbWall.Checked = true;
            }

			lstEntityList.Items.Clear();

			List<Entity> entList = m_ES.GetAllEntitiesPossesing();

			if(entList != null)
			{
				lstEntityList.Items.AddRange(entList.ToArray());
			}

			lstEntityList.SelectedItem = selectedEntity;
		}

		private void lstEntityList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstEntityList.SelectedItem != null)
			{
				tabComponents.TabPages.Clear();

				selectedEntity = (Entity)lstEntityList.SelectedItem;

                BFECore.EntitySystem.Systems.EditingSystem.selectedEntity = selectedEntity;

				lblEntityID.Text = string.Format("EntityID: {0}", selectedEntity.EntityID);
				txtEntityName.Text = selectedEntity.EntityName;


				//Load components into tab control
				List<Type> currentEntitiesTypes = m_ES.GetAllEntityTypes(selectedEntity);

				foreach (Type compType in currentEntitiesTypes)
				{
					TabPage compTab = new TabPage(SimpleObjectName(compType.ToString()));

					PropertyGrid pg = new PropertyGrid();					            
					pg.Dock = DockStyle.Fill;
					pg.SelectedObject = m_ES.GetComponent(selectedEntity, compType);

					compTab.Controls.Add(pg);
					tabComponents.TabPages.Add(compTab);
				}
			}
		}

		private void btnApplyEntityChanges_Click(object sender, EventArgs e)
		{
			if (lstEntityList.SelectedItem != null)
			{
				Entity tempE = (Entity)lstEntityList.SelectedItem;
				tempE.EntityName = txtEntityName.Text;

				UpdateList();

				lstEntityList.SelectedItem = tempE;
			}
		}

		private void btnAddComponent_Click(object sender, EventArgs e)
		{
			if (lstEntityList.SelectedItem != null)
			{
				Entity tempE = (Entity)lstEntityList.SelectedItem;
				if(ddlComponents.SelectedItem != null)
				{
					if ( !m_ES.HasComponent(tempE, (Type)ddlComponents.SelectedItem ) )
					{
						BaseComponent comp = (BaseComponent)Activator.CreateInstance((Type)ddlComponents.SelectedItem);
						m_ES.RegisterComponent(tempE, comp);

						UpdateList();

						lstEntityList.SelectedItem = tempE;

						foreach (TabPage tp in tabComponents.TabPages)
						{
							if (tp.Text == SimpleObjectName(ddlComponents.Text)) 
							{
								tabComponents.SelectedTab = tp;
								break;
							}
						}
					}
				}
			}
		}

		private void btnDeleteComponent_Click(object sender, EventArgs e)
		{
			if (lstEntityList.SelectedItem != null)
			{
				Entity tempE = (Entity)lstEntityList.SelectedItem;
				if (tabComponents.SelectedTab != null)
				{
					BaseComponent comp =  m_ES.GetComponent(tempE, BaseComponent.GetType(tabComponents.SelectedTab.Text));
					m_ES.RemoveComponent(tempE, comp);

					UpdateList();
				}
			}
		}

		private void txtEntityName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				btnAddEntity.Focus();
		}

		private void btnCreateAssemblage_Click(object sender, EventArgs e)
		{
			if (ddlAssemblages.SelectedItem != null)
			{
				selectedEntity = Assemblages.AssemblageInstructions[ddlAssemblages.Text]();
				UpdateList();
				txtEntityName.Focus();
			}
		}

        private string SimpleObjectName(string complexObjectName)
        {
            return complexObjectName.Substring(complexObjectName.LastIndexOf('.') + 1);
        }

        private void rbEntity_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEntity.Checked)
            {
                m_ES.EditType = EditorType.EntityMode;
            }
            else if (rbWall.Checked)
            {
                m_ES.EditType = EditorType.WallMode;
            }
        }

        private void cboClickTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClickTo.Text == "None")
            {
                m_ES.GridType = GridType.GRID_NONE;
            }
            else
            {
                m_ES.GridType = (GridType)int.Parse(cboClickTo.Text);
            }
        }
	}
}
