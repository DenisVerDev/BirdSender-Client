   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MessangerClientMessangerClient  C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls       MessangerClient.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:MessangerClient     ;controls&clr-namespace:MessangerClient.Controls       � Title$  
BirdSender��$	��450��$	��800��$
��1200��$	��500�� � WindowStartupLocation$ CenterScreen=� � AllowsTransparency .  � 
ResizeMode$ CanResizeWithGrip=� � WindowStyle$
 None=� � 	Resources  �� Source�� $ Controls/Dictionary1.xaml?�%    .��]� ���#F2F2F2.+�%   MessangerClient.Controls.TopBar     TopBarHeader$ 
BirdSender��   
ResizeMode . $	��Top=�$	��Top=�]� .+�&    MessangerClient.Controls.InfoBar  -   	  Name$	 infobar��$	��Top=�$	��Top=�*   $MessangerClient.Controls.ChannelList  -   
  Name$
 channellist��$
��Left=�$
��Left=��� $��1��$
��Left=����#E6E6E6� .+��� -    ��Name$ viewer��$��0,0,0,60q�$	��Top=� 
  scroll#��[  $��Disabled=�$
��Auto=�.���� -    ��Name$ msgcon�� �� Orientation$ Vertical=����#F2F2F2+   %MessangerClient.Controls.UserEnterMsg  -     Name$ userentermsg��$y�Hidden=�$��Bottom=�