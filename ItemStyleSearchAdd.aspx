<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItemStyleSearchAdd.aspx.cs" Inherits="ItemStyleSearchAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper"> <section class="content-header">
        <h1>Item Style Search
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="ItemStyleSearchAdd.aspx">Item Style Search</a></li>
        </ol>
    </section>
           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
    <section class="content">
        <!-- Default box -->
        <div class="box">
            <div class="box-body">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">Item Style Search</h3>

                        <div class="box-tools pull-right">
                            <asp:LinkButton ID="addNewStyle" runat="server" CssClass="btn btn-sm btn-facebook" OnClick="addNewStyle_Click"><i class="fa fa-plus-square"></i> Add New Style</asp:LinkButton>

                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <asp:Panel ID="Panel3" runat="server" DefaultButton="btnShowAll">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                        
                        <div class="row">
                           
                         <table class="table table-bordered">
                             <tr>
                                 <th>
                                     Vertical
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat2" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat3" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat4" runat="server" Text=""></asp:Label>
                                 </th>
                                 <th>
                                     <asp:Label ID="lblCat5" runat="server" Text=""></asp:Label>
                                 </th>
                                <th>
                                     Style Code
                                 </th>
                             </tr>
                            
                             <tr id="showCat" runat="server">
                                 <td>
                                     <asp:DropDownList ID="drp_itemCategory" runat="server" AutoPostBack="true" DataValueField="ItemCategoryID" DataTextField="ItemCategory" CssClass="form-control select2" OnSelectedIndexChanged="drp_itemCategory_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catTwo" runat="server" AutoPostBack="true" DataValueField="Cat2ID" DataTextField="C2Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catTwo_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catThree" runat="server" AutoPostBack="true" DataValueField="Cat3ID" DataTextField="C3Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catThree_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catFour" runat="server" AutoPostBack="true" DataValueField="Cat4ID" DataTextField="C4Name" CssClass="form-control select2" OnSelectedIndexChanged="drp_catFour_SelectedIndexChanged"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:DropDownList ID="drp_catFive" runat="server" AutoPostBack="true" DataValueField="Cat5ID" DataTextField="C5Name" CssClass="form-control select2"></asp:DropDownList>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="drpStyles" runat="server" CssClass="form-control"></asp:TextBox>
                                 </td>
                                 
                             </tr>
                             
                             <tr id="searchLabel" runat="server" visible="false">
                                 <td id="drp_itemCategorylbl"></td>
                                 <td id="drp_catTwolbl"></td>
                                 <td id="drp_catThreelbl"></td>
                                 <td id="drp_catFourlbl"></td>
                                 <td id="drp_catFivelbl"></td>
                                 <td id="drpStyleDisplay"></td>
                             </tr>
                         </table>
                                
                        </div>
                        <div class="box-body table-responsive">
                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                <asp:Button ID="btnRefresh"  class="btn btn-info pull-right btn-round" runat="server" Text="Refresh" OnClick="btnRefresh_Click"/>
                                
                                <asp:Button ID="btnFilter"  class="btn btn-warning pull-right btn-round" runat="server" Text="Filter" OnClick="btnFilter_Click"/>
                                <asp:Button ID="btnShowAll"  class="btn btn-primary pull-right btn-round" runat="server" Text="Show All" OnClick="btnShowAll_Click"/>
                            </div>
                        </div>
                            </ContentTemplate>
                             
                        </asp:UpdatePanel>
                        </asp:Panel>
                      

                        <div class="row" runat="server" visible="false" id="filterHideShow">
                            <div class="col-md-6 col-xs-12 col-lg-6">
                            <table class="table table-bordered table-striped">
                                <tr>
                                    <th>Select</th>
                                    <th>Data Field Value</th>
                                    <th>Search Text</th>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="controlNameTitle" runat="server" value="Title"/></td>
                                    <td>Title</td>
                                    <td><asp:TextBox ID="titleSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
                                </tr>
                                <asp:Repeater ID="rpt_DataField" runat="server" OnItemDataBound="rpt_DataField_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="datafieldHideShow" runat="server">
                                           <td>
                                               <asp:Label ID="IsAssignedDataField" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:CheckBox ID="controlName" runat="server" value='<%# Eval("ColumnNo").ToString() %>'/></td>
                                           <td><%# Eval("SettingName").ToString() %></td>
                                           <td><asp:TextBox ID="controlSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
                                       </tr>
                                   </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            </div>
                            <div class="col-md-6 col-xs-12 col-lg-6">
                                <table class="table table-bordered table-striped">
                                    <tr>
                                        <th>Select</th>
                                        <th>DropDown Value</th>
                                        <th>DropDown Search</th>
                                    </tr>
                                     <asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="drpHideShow" runat="server">
                                           <td>
                                               <asp:Label ID="IsAssigneddrp" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:CheckBox ID="drpName" runat="server" value='<%# Eval("checkValue").ToString() %>'/></td>
                                           <td><%# Eval("SettingName").ToString() %></td>
                                           <td>
                                               <asp:DropDownList ID="drpCols" runat="server" CssClass="form-control select2" DataTextField="Name" DataValueField="ID"></asp:DropDownList>
                                           </td>
                                       </tr>
                                        </ItemTemplate>
                                </asp:Repeater>
                                </table>
                            </div>
                        <div class="box-body table-responsive ">
                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                                <asp:Button ID="searchbyFilter"  class="btn btn-primary pull-right btn-round" runat="server" Text="Search" OnClick="searchbyFilter_Click"/>
                               
                            </div>
                        </div>
                        </div>
                           
                        </div>
                        <!-- /.row -->

                    <div class="row" runat="server">
                       <div class="col-md-12 col-xs-12 col-lg-12" visible="false" runat="server" id="searchData">
                           <div>
                               <asp:label runat="server" ID="successfailure" Text="" Visible="false" style="color:crimson;text-align:center;"></asp:label>
                           </div>
                           <table class="table table-bordered table-hover table-striped dtSearch">
                               <thead>
                                   <tr>
                                       <th>StyleID</th>
                                       <th>Title</th>
                                       <th>MRP</th>
                                       <th>StyleCode</th>
                                       <th>RFL</th>
                                       <th>NR</th>
                                    <%-- <th>EAN</th>--%>
                                       <th>View</th>
                                       <th>Edit</th>
                                   </tr>
                                </thead>
                               <tbody>
                               <asp:Repeater ID="rpt_Style" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("StyleID").ToString() %></td>
                                        <td>
                                            <asp:Label ID="StyleID" Visible="false" runat="server" Text='<%# Eval("StyleID").ToString() %>'></asp:Label>
                                            <asp:Label ID="title" runat="server" Text='<%# Eval("Title").ToString() %>'></asp:Label>
                                            <asp:Label ID="stylecode" Visible="false" runat="server" Text='<%# Eval("StyleCode").ToString() %>'></asp:Label>
                                            <asp:Label ID="lbltemcatid" Visible="false" runat="server" Text='<%# Eval("ItemCatID").ToString() %>'></asp:Label>
                                            
                                        </td>
                                        <td><asp:Label ID="mrp" runat="server" Text='<%# Eval("mrp").ToString() %>'></asp:Label></td>
                                        <td><%# Eval("StyleCode").ToString() %></td>
                                        <td>
                                            <asp:LinkButton ID="btnRFL" runat="server" OnClick="btnRFL_Click" CssClass="btn btn-sm btn-success"><i class="fa fa-list"></i> RFL</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnNR" runat="server" OnClick="btnNR_Click" CssClass="btn btn-sm btn-danger"><i class="fa fa-bug"></i> NR</asp:LinkButton>
                                        </td>
                                        <%--<td>
                                            <asp:LinkButton ID="btnEAN" runat="server"  CssClass="btn btn-sm bg-maroon" OnClick="btnEAN_Click"><i class="fa fa-barcode"></i> EAN</asp:LinkButton>
                                        </td>--%>
                                        <td>
                                            <asp:LinkButton ID="btnItemStyleDetails" runat="server" OnClick="btnItemStyleDetails_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        </td>
                                         <td>
                                            <asp:LinkButton ID="btnEditItemStyle" runat="server" OnClick="btnEditItemStyle_Click" CssClass="btn btn-sm bg-purple"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                        </td>
                                        
                                    </tr>
                                </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                           </table>
                           
                       </div>
                        <div class="col-md-12 col-xs-12 col-lg-12" visible="false" runat="server" id="Error">
                           <h3 style="color:red;">NO DATA FOUND</h3> 
                        </div>
                    </div>
                        

                

                    
                <!-- /.box -->
            </div>

            <!-- Default box -->
        </div>  <!-- Default box -->
        </div>

      <asp:Panel ID="Panel1" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button2" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender2"
                    runat="server"
                    TargetControlID="Button2"
                    PopupControlID="Panel1"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe1">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title"><asp:Label ID="lotTitle" runat="server" Text=''></asp:Label>
                               
                            </h4>
                        </div>
                        <div class="modal-body table-responsive">
                   <p> Please Add Active LOTS</p>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button3" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup1()" />
                  
        
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>

      <asp:Panel ID="Panel2" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button4" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender3"
                    runat="server"
                    TargetControlID="Button4"
                    PopupControlID="Panel2"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe2">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title">Please Add Piece/Packet for <asp:Label ID="lblSizeTitle" runat="server" Text='' style="color:brown;"></asp:Label>
                                 
                            </h4>
                        </div>
                        <div class="modal-body table-responsive">
                   
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button5" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup2()" />
                  
        
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>

         <asp:Panel ID="pnlDetails" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender1"
                    runat="server"
                    TargetControlID="Button1"
                    PopupControlID="pnlDetails"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title"><asp:Label ID="lblTitle" runat="server" Text=''></asp:Label>
                                <asp:Label ID="lblstyleIDHidden" visible="false" runat="server" Text=''></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                            <div>
                                <asp:label ID="rflError" Visible="false" runat="server" style="color:crimson;text-align:center;"></asp:label>
                            </div>
                        
                        <table class="table table-bordered table-striped">
                        <tr>
                            <th>Lot Pieces</th><th>Barcoded Pieces</th><th>Available Pieces</th>
                        </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lotPieces"></asp:Label></td>
                                <td><asp:Label runat="server" ID="barcodePiece"></asp:Label></td>
                                <td><asp:Label runat="server" ID="avlPiece"></asp:Label></td>
                            </tr>
                       </table>
                       <table class="table table-bordered table-striped">
                          <%-- <asp:label runat="server" id="scrollDownLotRpt"></asp:label> --%>
                           <tr>
                                <th width="2%">Select </th>
                                <th>LOT</th>
                            </tr>
                            <asp:Repeater ID="lot_RPT" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lotLblHdn" Visible="false" runat="server" Text='<%# Eval("BagId").ToString() %>'></asp:Label>
                                            <asp:Label ID="lotPiece" Visible="false" runat="server" Text='<%# Eval("totalPiece").ToString() %>'></asp:Label>
                                            <asp:RadioButton ID="lotRadio" AutoPostBack="true" runat="server"  OnCheckedChanged="lotRadio_CheckedChanged" ></asp:RadioButton>
                                        </td>
                                        <td><%# Eval("BagDescription").ToString() %></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="form-group">
                            <label>MRP</label>
                            <asp:TextBox ID="mrpRfl" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                            <div class="form-group">
                            <label>Purchase Rate</label>
                            <asp:TextBox ID="purchaseRfl" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                            <div class="form-group" style="display:none;">
                            <label>MFG Date</label>
                            <asp:TextBox ID="rflMfgDate" runat="server"  CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                            <div class="form-group" style="display:none;">
                            <label>Old Barcode</label>
                            <asp:TextBox ID="oldBarcode" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>

                            <%--<div class="form-group">
                            <label>Piece Per Packet</label>
                            <asp:TextBox ID="piecePerPacketRFL" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>--%>

                        <div class="form-group">
                            <label>Total Barcode</label>
                            <asp:TextBox ID="totalBarcodeRFL" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>

                            <div class="form-group">
                            <label>Sample ?</label>
                            <asp:RadioButtonList ID="isSampleRFL" runat="server"  RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                            
                            <table class="table table-bordered table-striped">
                            <tr>
                                <th width="2%">Select </th>
                                <th>Piece/Packet</th>
                            </tr>
                            <asp:Repeater ID="size_Rpt" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="sizeLblHdn" Visible="false" runat="server" Text='<%# Eval("SizeID").ToString() %>'></asp:Label>
                                            <asp:RadioButton ID="sizeRadio" AutoPostBack="true" runat="server"  OnCheckedChanged="sizeRadio_CheckedChanged" />
                                        </td>
                                        <td><%# Eval("Size1").ToString() %></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>
                            
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="btnOk" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup()" />
                  
                  <asp:LinkButton ID="SaveRFL" runat="server" CssClass="btn btn-success pull-right" OnClick="SaveRFL_Click" OnClientClick="return confirm('Do you Want to Save ?');">Save RFL</asp:LinkButton>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>


        <asp:Panel ID="NRPanel" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender4"
                    runat="server"
                    TargetControlID="Button6"
                    PopupControlID="NRPanel"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe4">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title"><asp:Label ID="lblNRTitle" runat="server" Text=''></asp:Label>
                                <asp:Label ID="lblSTtyleNRId" runat="server" Text=''></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                             <div>
                                <asp:label ID="nrError" Visible="false" runat="server" style="color:crimson;text-align:center;"></asp:label>
                            </div>
                        
                        <table class="table table-bordered table-striped">
                        <tr>
                            <th>Lot Pieces</th><th>Barcoded Pieces</th><th>Available Pieces</th>
                        </tr>
                            <tr>
                                <td><asp:Label runat="server" ID="lotPiecesN"></asp:Label></td>
                                <td><asp:Label runat="server" ID="barcodePieceN"></asp:Label></td>
                                <td><asp:Label runat="server" ID="avlPieceN"></asp:Label></td>
                            </tr>
                       </table>

                       <table class="table table-bordered table-striped">
                            <tr>
                                <th width="2%">Select </th>
                                <th>LOT</th>
                            </tr>
                            <asp:Repeater ID="lot_RPTNR" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lotLblHdn" Visible="false" runat="server" Text='<%# Eval("BagId").ToString() %>'></asp:Label>
                                            <asp:Label ID="lotPiece" Visible="false" runat="server" Text='<%# Eval("totalPiece").ToString() %>'></asp:Label>
                                            <asp:RadioButton ID="lotRadio" AutoPostBack="true" runat="server"  OnCheckedChanged="lotRadio_CheckedChangedNR" ></asp:RadioButton>
                                        </td>
                                        <td><%# Eval("BagDescription").ToString() %></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>

                            <div class="form-group">
                            <label>Reason</label>
                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="NRM">Minor</asp:ListItem>
                                <asp:ListItem Value="NRD">Damaged</asp:ListItem>
                                <asp:ListItem Value="NRR">Retail</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                       <div class="form-group">
                            <label>MRP</label>
                            <asp:TextBox ID="mrpNR" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                            <div class="form-group">
                            <label>Purchase Rate</label>
                            <asp:TextBox ID="purchaseNR" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                             <div class="form-group" style="display:none;">
                            <label>MFG Date</label>
                            <asp:TextBox ID="nrMfgDate" runat="server"  CssClass="form-control datepicker"></asp:TextBox>
                        </div>
                            <div class="form-group" style="display:none;">
                            <label>Old Barcode</label>
                            <asp:TextBox ID="oldBarcodeNR" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>
                                 <%--<div class="form-group">
                            <label>Piece Per Packet</label>
                            <asp:TextBox ID="piecePerPacketNRR" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>--%>

                        <div class="form-group">
                            <label>Total Barcode</label>
                            <asp:TextBox ID="totalBarcodeNRR" runat="server"  CssClass="form-control"></asp:TextBox>
                        </div>

                            <div class="form-group">
                            <label>Sample ?</label>
                            <asp:RadioButtonList ID="isSampleNRR" runat="server"  RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                            <table class="table table-bordered table-striped">
                            <tr>
                                <th width="2%">Select </th>
                                <th>Piece/Packet</th>
                            </tr>
                            <asp:Repeater ID="size_RptNR" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="sizeLblHdn" Visible="false" runat="server" Text='<%# Eval("SizeID").ToString() %>'></asp:Label>
                                            <asp:RadioButton ID="sizeRadio" AutoPostBack="true" runat="server"  OnCheckedChanged="sizeRadio_CheckedChangedNR" />
                                        </td>
                                        <td><%# Eval("Size1").ToString() %></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button7" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup4()" />
                  
                  <asp:LinkButton ID="saveNR" runat="server" CssClass="btn btn-success pull-right" OnClick="saveNR_Click" OnClientClick="return confirm('Do you Want to Save ?');">Save NR</asp:LinkButton>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>

        <asp:Panel ID="PanelDets" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button8" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender7"
                    runat="server"
                    TargetControlID="Button8"
                    PopupControlID="PanelDets"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe7">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title">
                           <asp:Label ID="lblItemNamedets" runat="server" Text=''></asp:Label>
                                <asp:Label ID="lblDetsStyleID" runat="server" Text=''></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                          
                        <table class="table table-bordered table-striped">
                            <tr>
                                <th>Item Category </th>
                                <th><asp:Label ID="DetItemCatname2" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname3" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname4" runat="server" Text=''></asp:Label> </th>
                                <th><asp:Label ID="DetItemCatname5" runat="server" Text=''></asp:Label> </th>
                                
                            </tr>
                            <tr>
                                <td><asp:Label ID="DetItemCatVal1" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal2" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal3" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal4" runat="server" Text=''></asp:Label></td>
                                <td><asp:Label ID="DetItemCatVal5" runat="server" Text=''></asp:Label></td>
                            </tr>
                             
                        </table>
                        <table class="table table-bordered table-striped" >
                                                        <tr>
                                    
                                                            <th>Data Field Value</th>
                                                            <th>Search Text</th>
                                                        </tr>
                                                        
                                                         <tr>
                                    
                                                            <td>MRP</td>
                                                            <td><asp:Label ID="detMrp" runat="server"></asp:Label></td>
                                                        </tr>
                                                        <asp:Repeater ID="rptDataFieldDets" runat="server" OnItemDataBound="rptDataFieldDets_ItemDataBound">
                                                           <ItemTemplate>
                                                               <tr id="datafieldHideShow" runat="server">
                                                                   <td><asp:Label ID="IsAssignedDataField" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label><%# Eval("SettingName").ToString() %></td>
                                                                   <td><asp:Label ID="controlSearch" runat="server" Text='<%# Eval("colVal").ToString() %>'></asp:Label></td>
                                                               </tr>
                                                           </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                        
                         <table class="table table-bordered table-striped">
                                    <tr>
                                        
                                        <th>DropDown Value</th>
                                        <th>DropDown Search</th>
                                    </tr>
                                     <asp:Repeater ID="rptDrop" runat="server" OnItemDataBound="rptDrop_ItemDataBound">
                                   <ItemTemplate>
                                       <tr id="drpHideShow" runat="server">
                                           
                                           <td>
                                               <asp:CheckBox ID="drpName" runat="server" value='<%# Eval("checkValue").ToString() %>' Visible="false"/>
                                               <%# Eval("SettingName").ToString() %></td>
                                           <td><asp:Label ID="IsAssigneddrp" runat="server" Visible="false" Text='<%# Eval("IsAssigned").ToString() %>'></asp:Label>
                                               <asp:Label ID="colName" runat="server" Text=''></asp:Label>
                                           </td>
                                       </tr>
                                        </ItemTemplate>
                                </asp:Repeater>
                                </table>

                             <table class="table table-bordered table-striped">
                                 <tr>
                                     <th>Image 1</th>
                                     <th>Image 2</th>
                                 </tr>
                                 <tr>
                                     <td>
                                         <asp:image ID="image1Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image2Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                  <tr>
                                     <th>Image 3</th>
                                     <th>Image 4</th>
                                 </tr>
                                  <tr>
                                     <td>
                                         <asp:image ID="image3Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image4Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                  <tr>
                                     <th>Image 5</th>
                                     <th>Image 6</th>
                                 </tr>
                                  <tr>
                                     <td>
                                         <asp:image ID="image5Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                     <td>
                                         <asp:image ID="image6Display" runat="server" imageurl="..." Visible="false" Height="45px" Width="45px"/>
                                     </td>
                                 </tr>
                                 </table>
                           
                       <asp:LinkButton ID="btnviewEAN" runat="server" OnClick="btnviewEAN_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-eye"></i> View EAN</asp:LinkButton>

              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="Button9" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup7()" />
                  
                  
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>


        



        <asp:Panel ID="EANpanel" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button10" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender5"
                    runat="server"
                    TargetControlID="Button10"
                    PopupControlID="EANpanel"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe10">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title" style="font-size:medium;font-weight:bold" >Title:&nbsp;<asp:Label ID="lbleantitle" runat="server" Text='' ForeColor="#0000ff" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                Style Code:&nbsp;<asp:Label ID="lbleanstylecode" runat="server" Text='' ForeColor="#ff0000" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                Category:&nbsp;<asp:Label ID="lblcat" runat="server" ForeColor="#cc0099" Font-Bold="true" Visible="true" Text=''></asp:Label>
                                 <asp:Label ID="lblstyleEANid" runat="server" ForeColor="#0000ff" Font-Bold="true" Visible="false" Text=''></asp:Label>
                                <asp:Label ID="lblEANitemcatid" runat="server" ForeColor="#0000ff" Font-Bold="true" Visible="false" Text=''></asp:Label>
                                <%--Style ID: &nbsp--%>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                             <div>
                                <asp:label ID="lblerror" Visible="false" runat="server" style="color:crimson;text-align:center;"></asp:label>
                            </div>
                        
                        


                        
                      
                            <table class="table table-bordered table-striped">
                            <tr>
                                <th>Size</th>
                                <th>EAN</th>
                            </tr>
                            <asp:Repeater ID="rpt_EAN" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblhdnid" Visible="false" runat="server" Text='<%# Eval("id").ToString() %>'></asp:Label>
                                            <asp:Label ID="sizeLblHdn" Visible="false" runat="server" Text='<%# Eval("SizeID").ToString() %>'></asp:Label>
                                            <%# Eval("Size1").ToString() %>
                                        </td>
                                        <td><asp:TextBox ID="txtEAN" runat="server" Text='<%# Eval("EAN").ToString() %>'   CssClass="form-control"></asp:TextBox></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="btncloseEAN" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup5()" />
                  
                  <asp:LinkButton ID="btnsaveEAN" runat="server" CssClass="btn btn-success pull-right" OnClick="btnsaveEAN_Click" OnClientClick="return confirm('Do you Want to Save ?');">Save EAN</asp:LinkButton>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>


        
        <asp:Panel ID="panelviewEAN" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button11" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender6"
                    runat="server"
                    TargetControlID="Button11"
                    PopupControlID="panelviewEAN"
                    BackgroundCssClass="modalBackground" BehaviorID="mpe11">
                </cc1:ModalPopupExtender>

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                           
                            <h4 class="modal-title" style="font-size:medium;font-weight:bold" >Title:&nbsp;<asp:Label ID="lbltitileviewEAN" runat="server" Text='' ForeColor="#0000ff" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                Style Code:&nbsp;<asp:Label ID="lblstylecodeviewEAN" runat="server" Text='' ForeColor="#ff0000" Font-Bold="true"></asp:Label>
                                 <asp:Label ID="lblViewEANStyleid" runat="server" ForeColor="#0000ff" Font-Bold="true" Visible="false" Text=''></asp:Label>
                                <%--Style ID: &nbsp--%>
                            </h4>
                        </div>
                        <div class="modal-body table-responsive" style="overflow-y:scroll;height:300px;">
                             <div>
                                <asp:label ID="lblerrpr" Visible="false" runat="server" style="color:crimson;text-align:center;"></asp:label>
                            </div>
                        
                        


                        
                      
                            <table class="table table-bordered table-striped">
                            <tr>
                                <th>Size</th>
                                <th>EAN</th>
                            </tr>
                            <asp:Repeater ID="rptViewEAN" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                           
                                            <%# Eval("Size1").ToString() %>
                                        </td>
                                        <td><%# Eval("EAN").ToString() %></td>
                                    </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                        </table>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="btncloseViewEAN" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup6()" />
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>
    </section>
  </ContentTemplate>
               
           </asp:UpdatePanel>
           </div>
</asp:Content>

