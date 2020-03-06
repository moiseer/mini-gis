using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TwoDimensionalFields.Maps;

namespace MiniGis
{
    public partial class LayersControl : UserControl
    {
        public MapControl MapControl;

        public LayersControl()
        {
            InitializeComponent();
        }

        public event EventHandler AddLayer;

        public int SelectedItemsCount => listView.SelectedItems.Count;

        public IEnumerable<ILayer> GetSelectedLayers()
        {
            return listView.SelectedItems.Cast<ListViewItem>()
                .Select(item=> item.Tag)
                .OfType<ILayer>();
        }

        public void UpdateLayers()
        {
            if (MapControl == null)
            {
                return;
            }

            listView.Items.Clear();

            listView.Items.AddRange(MapControl.Layers.Select(layer => new ListViewItem
            {
                Text = layer.Name,
                Checked = layer.Visible,
                Selected = layer.Selected,
                Tag = layer
            }).Reverse().ToArray());

            foreach (ListViewItem listViewItem in listView.Items)
            {
                if (listViewItem.Tag is ILayer layer)
                {
                    layer.Visible = listViewItem.Checked;
                }
            }

            CheckButtons();
        }

        private void buttonAdd_Click(object sender, EventArgs e) => AddLayer?.Invoke(sender, e);
        private void buttonDown_Click(object sender, EventArgs e) => MoveSelectedLayerDown();
        private void buttonRemove_Click(object sender, EventArgs e) => RemoveSelectedLayers();
        private void buttonUp_Click(object sender, EventArgs e) => MoveSelectedLayerUp();

        private void CheckButtons()
        {
            ButtonRemove.Enabled = listView.SelectedItems.Count > 0;
            ButtonUp.Enabled = listView.SelectedItems.Count == 1 && listView.SelectedItems[0].Index > 0;
            ButtonDown.Enabled = listView.SelectedItems.Count == 1 && listView.SelectedItems[0].Index < listView.Items.Count - 1;
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is ILayer layer)
            {
                layer.Visible = !layer.Visible;
                MapControl.Invalidate();
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Tag is ILayer layer)
            {
                layer.Selected = e.IsSelected;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e) => CheckButtons();

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

            MapControl.MoveLayerDown(listView.SelectedItems[0].Tag as ILayer);
            UpdateLayers();
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

            MapControl.MoveLayerUp(listView.SelectedItems[0].Tag as ILayer);
            UpdateLayers();
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

            foreach (var layer in listView.SelectedItems.Cast<ListViewItem>().Select(item => item.Tag).OfType<ILayer>())
            {
                MapControl.RemoveLayer(layer);
            }

            MapControl.Invalidate();
            UpdateLayers();
        }
    }
}
