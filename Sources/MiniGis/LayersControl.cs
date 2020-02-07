using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TwoDimensionalFields.Maps;

namespace MiniGis
{
    public partial class LayersControl : UserControl
    {
        public LayersControl()
        {
            InitializeComponent();
        }

        public MapControl MapControl;

        public int SelectedItemsCount
        {
            get { return listView.SelectedItems.Count; }
        }

        public event EventHandler AddLayer;

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!(e.Item.Tag is ILayer layer))
            {
                return;
            }
            
            layer.Visible = !layer.Visible;
            MapControl.Invalidate();
        }

        public void UpdateLayers()
        {
            if (MapControl == null)
            {
                return;
            }
            
            listView.Items.Clear();
            
            foreach (var listViewItem in MapControl.Layers.Select(layer => new ListViewItem
            {
                Text = layer.Name, 
                Checked = layer.Visible, 
                Selected = layer.Selected, 
                Tag = layer
            }))
            {
                listView.Items.Insert(0, listViewItem);
                if (listViewItem.Tag is ILayer tmpLayer)
                {
                    tmpLayer.Visible = listViewItem.Checked;
                }
            }
            
            CheckButtons();
        }

        private void RemoveSelectedLayers()
        {
            if (MapControl == null)
            {
                return;
            }
            
            if (listView.SelectedItems.Count == 0)
            {
                return;
            }
            
            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is ILayer layer)
                {
                    MapControl.RemoveLayer(layer);
                }
            }
            
            UpdateLayers();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedLayers();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddLayer?.Invoke(sender, e);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            MoveSelectedLayerUp();
        }

        private void MoveSelectedLayerUp()
        {
            if (MapControl == null)
            {
                return;
            }

            if (listView.SelectedItems.Count != 1)
            {
                return;
            }
            
            MapControl.MoveLayerUp(listView.SelectedItems[0].Tag as Layer);
            
            UpdateLayers();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            MoveSelectedLayerDown();
        }

        private void MoveSelectedLayerDown()
        {
            if (MapControl == null)
            {
                return;
            }

            if (listView.SelectedItems.Count != 1)
            {
                return;
            }
            
            MapControl.MoveLayerDown(listView.SelectedItems[0].Tag as Layer);
            UpdateLayers();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckButtons();
        }

        private void CheckButtons()
        {
            ButtonRemove.Enabled = listView.SelectedItems.Count > 0;
            ButtonUp.Enabled = listView.SelectedItems.Count == 1 && listView.SelectedItems[0].Index > 0;
            ButtonDown.Enabled = listView.SelectedItems.Count == 1 && listView.SelectedItems[0].Index < listView.Items.Count - 1;
        }

        public List<ILayer> GetSelectedLayers()
        {
            var layers = new List<ILayer>();
            if (listView.SelectedItems.Count == 0) return layers;
            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is ILayer layer)
                {
                    layers.Add(layer);
                }
            }
            return layers;
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Tag is ILayer layer)
            {
                layer.Selected = e.IsSelected;
            }
        }
    }
}
