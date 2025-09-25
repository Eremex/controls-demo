# TreeList and TreeView Controls

The Eremex Controls library includes two data-aware controls to display hierarchical data in the form of a tree — `TreeListControl` and `TreeViewControl`. They render data source items as hierarchical nodes (rows).

`TreeList` control supports multiple columns:

![treelist](images/treelist.png)


`TreeView` control is a single-column component:

![treeview](images/treeview.png)



`TreeListControl` and `TreeViewControl` have one ancestor, so they share multiple features:

- Data Binding — You can bind the controls to self-referential (flat) and hierarchical data sources.
- Unbound Mode — Allows you to manually create the node structure.
- Built-in Node Checkboxes — Allow you to select individual nodes.
- Data Sorting — Allows you to sort sibling nodes in ascending or descending order. TreeList supports data sorting against one or multiple columns.
- Node Images — Allow you to display custom images before cell values in the hierarchy column.
- Styles — Allow you to customize the appearance settings of the controls' elements in various states.
- Search Panel — Helps a user quickly locate nodes by the data they contain.
- Data Edit Operations — A user can edit cell values if data editing is enabled. You can embed Eremex and custom editors in cells to edit and present cell values in a specific manner.
- Data Validation — The validation mechanism helps you check a user's input and data source's values, and show errors in cells.
- Built-in and Custom Context Menus
- Data Annotation Attribute Support — The TreeList and TreeView controls take into account dedicated Data Annotation attributes applied to the data source's properties. You can use Data Annotation attributes to specify custom visibility, position, read-only state, and display name for auto-generated columns.
- Node Drag-and-Drop - A user can drag a node within the control and to another control.
- High Performance for Large Volumes of Data — The data virtualization mechanism for vertical and horizontal scrolling boosts the control's performance when displaying large numbers of rows and columns.

TreeList-specific features include:

- Unbound Columns — You can add unbound columns (those that are not bound to data source fields) and populate them with data manually, using an event.
- Column Resize and Move Operations
- Auto Filter Row — A special row that allows a user to filter data against columns.
- Column Header Templates – Allow you to display custom content in column headers, including images.
- Multiple Node Selection (Highlight) — You can enable multiple node selection mode to allow a user to select (highlight) multiple nodes at one time.
- Column Bands — Allow you to visually group multiple columns under a common header.
- Export to XLSX Format

## Documentation

- [TreeList and TreeView](https://eremexcontrols.net/controls/treelist/)
