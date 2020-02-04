<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInventorySearch.aspx.cs" Inherits="ViewInventorySearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="../../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />

    <!-- iCheck for checkboxes and radio inputs -->
    <link rel="stylesheet" href="../../plugins/iCheck/all.css" />

    <!-- Morris chart -->
    <link rel="stylesheet" href="bower_components/morris.js/morris.css" />
    <!-- jvectormap -->
    <link rel="stylesheet" href="bower_components/jvectormap/jquery-jvectormap.css" />
    <!-- Date Picker -->
    <link rel="stylesheet" href="bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Daterange picker -->
    <link rel="stylesheet" href="bower_components/bootstrap-daterangepicker/daterangepicker.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../../bower_components/select2/dist/css/select2.min.css" />
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" />

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />

    <script type="text/javascript">
        function firedtSearch() {
            $(".dtSearch").DataTable({
                'order': [0, 'desc']

            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

		<div class="" style="background-color: #ecf0f5;">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>View Inventory
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">View Inventory</li>
                </ol>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="example11" class="table table-bordered table-striped dtSearch">
                                    <thead>
                                        <tr>                                       
                                            <th>List</th>
                                            <th>Sell</th>                                         
                                            <th>Style Code</th>
                                            <th>Available Size</th>
                                            <th>Listed Status</th>                                         
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptViewInventory" runat="server" OnItemDataBound="rptViewInventory_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton1" CommandName="edit" class="btn btn-primary" data-toggle="modal" data-target="#modal1" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                            runat="server"><i class="icon-remove"></i>List</asp:LinkButton></td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton2" CommandName="edit" class="btn btn-danger" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                            runat="server"><i class="icon-remove"></i>Sold</asp:LinkButton></td>
                                                  
                                                    <td><asp:Label ID="lblStyleId" runat="server" Text='<%# Eval("StyleID")%>' Visible="false"></asp:Label><%# Eval("StyleCode")%> </td>
                                                    <td><asp:Label ID="lblSizeDet" runat="server" Text=""></asp:Label></td>
                                                    <td><asp:Label ID="lblListDet" runat="server" Text=""></asp:Label></td>

                                                
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

				<div class="row">
				<div class="col-xs-12">
					<div>
						<div class="box-body table-responsive ">
							<table class="table table-bordered table-stripped">
								<tr>
									<td>
										<asp:LinkButton ID="lbFirst" runat="server" CssClass=""
											OnClick="lbFirst_Click">First</asp:LinkButton>
									</td>
									<td>
										<asp:LinkButton ID="lbPrevious" runat="server"
											OnClick="lbPrevious_Click">Previous</asp:LinkButton>
									</td>
									<td>
										<asp:DataList ID="rptPaging" runat="server"
											OnItemCommand="rptPaging_ItemCommand"
											OnItemDataBound="rptPaging_ItemDataBound"
											RepeatDirection="Horizontal">
											<ItemTemplate>
												<asp:Button class="btn btn-success" BackColor="#999999" BorderColor="#999999" ID="lbPaging" runat="server"
													CommandArgument='<%# Eval("PageIndex") %>'
													CommandName="newPage"
													Text='<%# Eval("PageText") %> '></asp:Button>
												&nbsp;
                                            
                                            
                                           
											</ItemTemplate>
										</asp:DataList>
									</td>

									<td>
										<asp:LinkButton ID="lbNext" runat="server"
											OnClick="lbNext_Click">Next</asp:LinkButton>
									</td>
									<td>
										<asp:LinkButton ID="lbLast" runat="server"
											OnClick="lbLast_Click">Last</asp:LinkButton>
									</td>
									<td>
										<asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
									</td>
								</tr>
							</table>
						</div>
					</div>

				</div>
			</div>

            </section>
        </div>

        <div class="" style="background-color: #ecf0f5;">
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>View Inventory
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">View Inventory</li>
                </ol>
            </section>
            <section class="content">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="box">
                            <div class="box-body table-responsive">
                                <table id="example1" class="table table-bordered table-striped dtSearch">
                                    <thead>
                                        <tr>
                                            <asp:HiddenField ID="hdnID" runat="server" />
                                            <th>List</th>
                                            <th>Sold</th>
                                            <th>Dispatch</th>
                                            <th>Barcode No</th>
                                            <th>Category</th>
                                            <th>Item Title</th>
                                            <th id="col1" runat="server" visible="false">
                                                <asp:Label ID="lblcol1" runat="server" Text=""></asp:Label></th>
                                            <th id="col2" runat="server" visible="false">
                                                <asp:Label ID="lblcol2" runat="server" Text=""></asp:Label></th>
                                            <th id="col3" runat="server" visible="false">
                                                <asp:Label ID="lblcol3" runat="server" Text=""></asp:Label></th>
                                            <th id="col4" runat="server" visible="false">
                                                <asp:Label ID="lblcol4" runat="server" Text=""></asp:Label></th>
                                            <th id="col5" runat="server" visible="false">
                                                <asp:Label ID="lblcol5" runat="server" Text=""></asp:Label></th>
                                            <th id="col6" runat="server" visible="false">
                                                <asp:Label ID="lblcol6" runat="server" Text=""></asp:Label></th>
                                            <th id="col7" runat="server" visible="false">
                                                <asp:Label ID="lblcol7" runat="server" Text=""></asp:Label></th>
                                            <th id="col8" runat="server" visible="false">
                                                <asp:Label ID="lblcol8" runat="server" Text=""></asp:Label></th>
                                            <th id="col9" runat="server" visible="false">
                                                <asp:Label ID="lblcol9" runat="server" Text=""></asp:Label></th>
                                            <th id="col10" runat="server" visible="false">
                                                <asp:Label ID="lblcol10" runat="server" Text=""></asp:Label></th>

                                            <th id="control1" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol1" runat="server" Text=""></asp:Label></th>
                                            <th id="control2" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol2" runat="server" Text=""></asp:Label></th>
                                            <th id="control3" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol3" runat="server" Text=""></asp:Label></th>
                                            <th id="control4" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol4" runat="server" Text=""></asp:Label></th>
                                            <th id="control5" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol5" runat="server" Text=""></asp:Label></th>
                                            <th id="control6" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol6" runat="server" Text=""></asp:Label></th>
                                            <th id="control7" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol7" runat="server" Text=""></asp:Label></th>
                                            <th id="control8" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol8" runat="server" Text=""></asp:Label></th>
                                            <th id="control9" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol9" runat="server" Text=""></asp:Label></th>
                                            <th id="control10" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol10" runat="server" Text=""></asp:Label></th>
                                            <th id="control11" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol11" runat="server" Text=""></asp:Label></th>
                                            <th id="control12" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol12" runat="server" Text=""></asp:Label></th>
                                            <th id="control13" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol13" runat="server" Text=""></asp:Label></th>
                                            <th id="control14" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol14" runat="server" Text=""></asp:Label></th>
                                            <th id="control15" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol15" runat="server" Text=""></asp:Label></th>
                                            <th id="control16" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol16" runat="server" Text=""></asp:Label></th>
                                            <th id="control17" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol17" runat="server" Text=""></asp:Label></th>
                                            <th id="control18" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol18" runat="server" Text=""></asp:Label></th>
                                            <th id="control19" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol19" runat="server" Text=""></asp:Label></th>
                                            <th id="control20" runat="server" visible="false">
                                                <asp:Label ID="lblcontrol20" runat="server" Text=""></asp:Label></th>
                                            <th>Size</th>
                                            <th>Rack Barcode</th>
                                            <th>Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="GV" runat="server" OnItemDataBound="rptr_ItemBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton1" CommandName="edit" class="btn btn-primary" data-toggle="modal" data-target="#modal1" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                            runat="server"><i class="icon-remove"></i>List</asp:LinkButton></td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton2" CommandName="edit" class="btn btn-danger" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                            runat="server"><i class="icon-remove"></i>Sold</asp:LinkButton></td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton3" CommandName="edit" class="btn box-warning" CausesValidation="False" CommandArgument='<%# Eval("StyleID") %>'
                                                            runat="server"><i class="icon-remove"></i>Dispatch</asp:LinkButton></td>
                                                    <td><%# Eval("BarcodeNo")%> </td>
                                                    <td><%# Eval("ItemCategory")%> </td>
                                                    <td><%# Eval("Title")%> </td>

                                                    <td id="col1" runat="server" visible="false"><%# Eval("C1Name")%> </td>
                                                    <td id="col2" runat="server" visible="false"><%# Eval("C2Name")%> </td>
                                                    <td id="col3" runat="server" visible="false"><%# Eval("C3Name")%> </td>
                                                    <td id="col4" runat="server" visible="false"><%# Eval("C4Name")%> </td>
                                                    <td id="col5" runat="server" visible="false"><%# Eval("C5Name")%> </td>
                                                    <td id="col6" runat="server" visible="false"><%# Eval("C6Name")%> </td>
                                                    <td id="col7" runat="server" visible="false"><%# Eval("C7Name")%> </td>
                                                    <td id="col8" runat="server" visible="false"><%# Eval("C8Name")%> </td>
                                                    <td id="col9" runat="server" visible="false"><%# Eval("C9Name")%> </td>
                                                    <td id="col10" runat="server" visible="false"><%# Eval("C10Name")%> </td>

                                                    <td id="control1" runat="server" visible="false"><%# Eval("Control1")%> </td>
                                                    <td id="control2" runat="server" visible="false"><%# Eval("Control2")%> </td>
                                                    <td id="control3" runat="server" visible="false"><%# Eval("Control3")%> </td>
                                                    <td id="control4" runat="server" visible="false"><%# Eval("Control4")%> </td>
                                                    <td id="control5" runat="server" visible="false"><%# Eval("Control5")%> </td>
                                                    <td id="control6" runat="server" visible="false"><%# Eval("Control6")%> </td>
                                                    <td id="control7" runat="server" visible="false"><%# Eval("Control7")%> </td>
                                                    <td id="control8" runat="server" visible="false"><%# Eval("Control8")%> </td>
                                                    <td id="control9" runat="server" visible="false"><%# Eval("Control9")%> </td>
                                                    <td id="control10" runat="server" visible="false"><%# Eval("Control10")%> </td>
                                                    <td id="control11" runat="server" visible="false"><%# Eval("Control11")%> </td>
                                                    <td id="control12" runat="server" visible="false"><%# Eval("Control12")%> </td>
                                                    <td id="control13" runat="server" visible="false"><%# Eval("Control13")%> </td>
                                                    <td id="control14" runat="server" visible="false"><%# Eval("Control14")%> </td>
                                                    <td id="control15" runat="server" visible="false"><%# Eval("Control15")%> </td>
                                                    <td id="control16" runat="server" visible="false"><%# Eval("Control16")%> </td>
                                                    <td id="control17" runat="server" visible="false"><%# Eval("Control17")%> </td>
                                                    <td id="control18" runat="server" visible="false"><%# Eval("Control18")%> </td>
                                                    <td id="control19" runat="server" visible="false"><%# Eval("Control19")%> </td>
                                                    <td id="control20" runat="server" visible="false"><%# Eval("Control20")%> </td>
                                                    <td><%# Eval("Size1")%> </td>
                                                    <td><%# Eval("RackBarcode")%> </td>
                                                    <td><%# Eval("Status")%> </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>

        <div class="modal fade" id="modal1" clientidmode="Static" runat="server">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Default Modal</h4>
                    </div>

                    <div class="modal-body">
                        <div class="form-group">
                            <label>Select Item Category</label>
                            <asp:DropDownList ID="ddlItemcategory" runat="server" DataTextField="name" DataValueField="id" class="form-control select2">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label>Item ID</label>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div id="dvrbd1" visible="true" style="padding:5px;" runat="server" class="col-md-12">
                            <asp:RadioButtonList ID="RadioButtonList1" CellPadding="15" CellSpacing="15" RepeatColumns="5" runat="Server">
                                <asp:ListItem Text="3 Days" Value="3 Days"></asp:ListItem>
                                <asp:ListItem Text="7 Days" Value="7 Days"></asp:ListItem>
                                <asp:ListItem Text="15 Days" Value="15 Days"></asp:ListItem>
                                <asp:ListItem Text="30 Days" Value="30 Days"></asp:ListItem>
                                 <asp:ListItem Text="Good Till Cancel" Value="Good Till Cancel"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                        <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload" />
                    </div>
                    <asp:Label ID="lblMessage" runat="server" />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <script src="bower_components/jquery/dist/jquery.min.js"></script>
        <!-- jQuery UI 1.11.4 -->
        <script src="bower_components/jquery-ui/jquery-ui.min.js"></script>
        <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
        <script>
            $.widget.bridge('uibutton', $.ui.button);
        </script>
        <!-- Bootstrap 3.3.7 -->
        <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
        <!-- DataTables -->
        <script src="../../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
        <script src="../../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
        <!-- Select2 -->
        <script src="../../bower_components/select2/dist/js/select2.full.min.js"></script>
        <!-- Morris.js charts -->
        <script src="bower_components/raphael/raphael.min.js"></script>
        <script src="bower_components/morris.js/morris.min.js"></script>
        <!-- Sparkline -->
        <script src="bower_components/jquery-sparkline/dist/jquery.sparkline.min.js"></script>
        <!-- jvectormap -->
        <script src="plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
        <script src="plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
        <!-- jQuery Knob Chart -->
        <script src="bower_components/jquery-knob/dist/jquery.knob.min.js"></script>
        <!-- daterangepicker -->
        <script src="bower_components/moment/min/moment.min.js"></script>
        <script src="bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
        <!-- datepicker -->
        <script src="bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
        <!-- Bootstrap WYSIHTML5 -->
        <script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
        <!-- Slimscroll -->
        <script src="bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
        <!-- iCheck 1.0.1 -->
        <script src="../../plugins/iCheck/icheck.min.js"></script>

        <!-- FastClick -->
        <script src="bower_components/fastclick/lib/fastclick.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/adminlte.min.js"></script>
        <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
        <script src="dist/js/pages/dashboard.js"></script>
        <!-- AdminLTE for demo purposes -->
        <script src="dist/js/demo.js"></script>

        <script>
            $(function () {
                $('#example1').DataTable()
                $('#example2').DataTable({
                    'paging': true,
                    'lengthChange': false,
                    'searching': false,
                    'ordering': true,
                    'info': true,
                    'autoWidth': false
                })
            })

            $("input[type=checkbox]").addClass("minimal").iCheck({
                checkboxClass: 'icheckbox_minimal-blue'
            })

            $("input[type=radio]").addClass("minimal").iCheck({
                radioClass: 'iradio_minimal-blue'
            })

        </script>
    </form>
</body>
</html>
