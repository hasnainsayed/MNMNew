<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Listpayment_file.aspx.cs" Inherits="Listpayment_file" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 139px;
        }
        .auto-style3 {
            width: 58px;
        }
        .auto-style4 {
            width: 226px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> 
         <section class="content-header">
        <h1>Payment_File List
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="invoice.aspx">Payment_File List</a></li>
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
                        <h3 class="box-title">Payment_File List</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                            <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                            <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th width="20%";>Search By</th>
                                                <th runat="server" id="hSearch">Search Text</th>
                                                <th runat="server" id="hVloc" visible="false">Virtual Location</th>
                                                <th runat="server" id="hFDate" visible="false">From Date</th>
                                                <th runat="server" id="hTdate" visible="false">To Date</th>

                                               
                                                <th>Search</th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <td width="20%";>
                                                    <asp:DropDownList ID="searchBy" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="searchBy_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1" Selected="True"> Barcode</asp:ListItem>
                                                        <asp:ListItem Value="2"> SalesID</asp:ListItem>
                                                        <asp:ListItem Value="3"> Virtual location</asp:ListItem>
                                                        <asp:ListItem Value="4"> Date Range</asp:ListItem>
                                                        <asp:ListItem Value="5"> -1</asp:ListItem>
                                                    </asp:DropDownList> 
                                                    </td>
                                                <td runat="server" id="tSearch"><asp:TextBox ID="searchField" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                <td runat="server" id="tVloc" visible="false">
                                                    <asp:DropDownList ID="vLoc" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID" AutoPostBack="true" OnSelectedIndexChanged="vLoc_SelectedIndexChanged"></asp:DropDownList></td>
                                                <td runat="server" id="tFDate" visible="false">
                                                    <asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                                <td runat="server" id="tTdate" visible="false">
                                                    <asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                </td>
                                                <td><asp:LinkButton ID="search" runat="server" OnClick="search_Click" CssClass="btn btn-sm btn-primary"><i class="fa fa-search"></i> Search</asp:LinkButton>&nbsp;&nbsp;
                                               <asp:LinkButton ID="btnexpoettoexcel" runat="server" CssClass="btn btn-sm btn-success"   OnClick="btnexpoettoexcel_Click" Visible="false"><i class="fa  fa-file-excel-o" ></i> Export</asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnaddmanual" runat="server" CssClass="btn btn-sm btn-warning"   OnClick="btnaddmanual_Click" Visible="false"><i class="fa  fa-plus" ></i> Add New Entry</asp:LinkButton>
                                                    
                                                </td>
                                                
                                                
                                                   <td><asp:FileUpload ID="FileUpload" runat="server"  Visible="false"  /></td> 
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnupload" runat="server" CssClass="btn btn-sm bg-purple"   OnClick="btnupload_Click" Visible="false"><i class="fa  fa-upload" ></i> Upload</asp:LinkButton></td>
                                                </tr>
                                
                                        </table>
                              <div id="sales" runat="server" visible="false">
                                
                            
                                 <div class="box-header with-border" id="divid" runat="server" visible="false">
                                     <bold>Following Barcodes Are Not Found :</bold><br />
                        <asp:Label ID="lblerrorbarcode" runat="server" Visible="false" Text="" Font-Bold="true" ForeColor="Red" Font-Size="Large"></asp:Label>
                                     </div>
                    
              
                   
                             <div class="box-body table-responsive">
                            <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                <tr>
                                   <th>Order Date</th>
                                    <th>Sales Id</th>
                                    <th>StockUp Id</th>
                                     <th>Channel</th>
                                     <th>Merchant SKU</th>
                                    <th>Product ID</th>
                                     <th>Product Name</th>
                                     
                                    <th>SP</th>
                                    <th>Payment Status</th>
                                    <th>Payment Type</th>
                                    <th>Payable Amount</th>
                                     <th>Select</th>
                                    <th>Update</th>
                                    <th>Delete</th>
                                    
                                </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rptsales" runat="server" OnItemDataBound="rptsales_ItemDataBound">
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:Label ID="salesid" runat="server" Visible="false" Text='<%# Eval("salesid").ToString() %>' ></asp:Label>
                                               <asp:Label ID="id" runat="server" Visible="false" Text='<%# Eval("Pt_id").ToString() %>' ></asp:Label>
                                               <%# Eval("order_date").ToString() %>
                                           </td>
                                           <td> <%# Eval("salesid").ToString() %></td>
                                           <td> <%# Eval("stockupId").ToString() %>
                                                 <asp:Label ID="lbltype" runat="server" Visible="false" Text='<%# Eval("type").ToString() %>' ></asp:Label>
                                           </td>
                                          
                                          <td> <%# Eval("Location").ToString() %></td>
                                          <td> <%# Eval("Merchant_SKU").ToString() %></td>
                                          <td> <%# Eval("productid").ToString() %></td>
                                          <td> <%# Eval("productname").ToString() %></td>
                                           <td> <%# Eval("sp").ToString() %></td>
                                           <td> <%# Eval("Payment_Status").ToString() %></td>
                                           <td> <%# Eval("Payment_Type").ToString() %></td>
                                            <%--<td> <%# Eval("Barcode").ToString() %></td>--%>
                                           <td> <%# Eval("Payable_Amoun").ToString() %></td>
                                           <td><asp:RadioButtonList ID="rbttype" class="form-control" runat="server" Width="93px" RepeatDirection="Horizontal" RepeatLayout="Flow" >
                                                <asp:ListItem Text="Stk" Value="stock"></asp:ListItem><asp:ListItem Text="Del"  Value="deleted" ></asp:ListItem>
                                               </asp:RadioButtonList>
                                               <%--<asp:RequiredFieldValidator id="myVal" ControlToValidate="rbttype" ErrorMessage="Required" runat="server" ValidationGroup="btnaction" />--%>
                                           </td>  
                                           <td>
                                               <div class="input-group input-group-md" style="width: 310px">
                                                  <asp:TextBox ID="txtstockupid" runat="server" CssClass="form-control"></asp:TextBox>
												<span class="input-group-btn">
											<asp:LinkButton ID="btnaction" runat="server" CssClass="btn btn-primary btn-flat" OnClick="btnaction_Click"  ><i class="fa fa-arrow-circle-o-up" ></i> Update</asp:LinkButton>														</span>
														</div>
					                      </td>
                                            <td>
														<asp:LinkButton ID="btndelete" runat="server" CssClass="btn btn-sm btn-danger" OnClick="btndelete_Click" ><i class="fa  fa-trash" ></i> Delete</asp:LinkButton>
															</td>
                                        


                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                                </div>
                                </div>

                            <div id="divmain" runat="server" visible="false">
                                
                    
              
                   
                             <div class="box-body table-responsive">
                            <table class="table table-bordered table-striped table-hover dtSearch">
                                <thead>
                                <tr>
                                    <th width="15%">Order Date</th>
                                    <th>Sales Id</th>
                                    <th>StockUp Id</th>
                                    <th>Type</th>
                                    <th>Channel</th>
                                    <th>Barcode</th>
                                    <th>Sales Price</th>
                                    <th>MRP</th>
                                    <th>Payment Status</th>
                                    <th>Payment Type</th>
                                    <th>Payable Amount</th>
                                     <th>Brand</th>
                                     <th>Title</th>
                                     <th>Size</th>
                                     <th>Article</th>
                                      <th>Invoice #</th>
                                     <th>Lot No #</th>
                                    

                                     
                                    
                                   
                                </tr>
                                    </thead>
                                <tbody>
                                <asp:Repeater ID="rpt_pymt" runat="server" >
                                   <ItemTemplate>
                                       <tr>
                                           <td>
                                               <asp:Label ID="id" runat="server" Visible="false" Text='<%# Eval("Pt_id").ToString() %>' ></asp:Label>
                                               <%# Eval("order_date").ToString() %>
                                           </td>
                                           <td> <%# Eval("salesid").ToString() %></td>
                                           <td> <%# Eval("stockupId").ToString() %></td>

                                            <td> <%# Eval("type").ToString() %></td>
                                            <td> <%# Eval("Location").ToString() %></td>
                                           <td> <%# Eval("BarcodeNo").ToString() %></td>
                                           <td> <%# Eval("sellingprice").ToString() %></td>
                                            <td> <%# Eval("sp").ToString() %></td>
                                           <td> <%# Eval("Payment_Status").ToString() %></td>
                                           <td> <%# Eval("Payment_Type").ToString() %></td>
                                           <td> <%# Eval("Payable_Amoun").ToString() %></td>
                                            <td> <%# Eval("brand").ToString() %></td>
                                            <td> <%# Eval("Title").ToString() %></td>
                                            <td> <%# Eval("Size1").ToString() %></td>
                                            <td> <%# Eval("Articel").ToString() %></td>
                                            <td> <%# Eval("invoiceid").ToString() %></td>
                                             <td> <%# Eval("Lotno").ToString() %></td>
                                          
                                         

                                       </tr>
                                       </ItemTemplate>
                                </asp:Repeater>
                                </tbody>
                            </table>
                                </div>
                                </div>
               


                        
                        </div>
             <%--      <asp:Panel ID="p1" runat="server" Style="display: none;">
                <!--height:50%!important;width:50%!important;-->

                <asp:Button ID="Button6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupExtender4"
                    runat="server"
                    TargetControlID="Button6"
                    PopupControlID="p1"
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
                             
                        
                        

                       <table class="table table-bordered table-striped">
                            <tr>
                                <th width="2%">Select </th>
                                <th><asp:DropDownList ID="drploc" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList></th>
                            </tr>
                            
                        </table>
              </div>
              <div class="modal-footer btn-toolbar">
                  <asp:Button ID="btnclose" CssClass="btn btn-danger pull-right" runat="server" Text="Close" OnClientClick="return HideModalPopup4()" />
                  
                  <asp:LinkButton ID="btngo" runat="server" CssClass="btn btn-success pull-right" OnClick="btngo_Click"   OnClientClick="return confirm('Do you Want to Save ?');">Go</asp:LinkButton>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
                <!-- /.modal-dialog -->
       


            </asp:Panel>--%>


                        </div></div></div>
        </section>
                                    </ContentTemplate>
                 <Triggers>
             <asp:PostBackTrigger ControlID="btnexpoettoexcel" />
             <asp:PostBackTrigger ControlID="btnupload" />
             
             
         </Triggers>
              
               </asp:UpdatePanel>

        </div>
</asp:Content>

