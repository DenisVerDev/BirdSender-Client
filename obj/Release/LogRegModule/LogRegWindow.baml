   M S B A M L   `   `   `  ���� J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MessangerClient.LogRegModuleMessangerClient.LogRegModule  C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  /    )MessangerClient.LogRegModule.LogRegWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:MessangerClient.LogRegModule     ;controls&clr-namespace:MessangerClient.Controls     b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgs&http://sharpvectors.codeplex.com/svgc/    � Title$  LogRegWindow��$	��420��$	��700�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � 	Resources  �� Source�� $" ../Controls/Dictionary1.xaml?�%    .��]� ���#F2F2F2.+�%   MessangerClient.Controls.TopBar  -     Name$ topbar��   TopBarHeader$ 
BirdSender��   
ResizeMode .  $	��Top=�$	��Top=�(  "SharpVectors.Converters.SvgViewbox  -   	  Name$
	 logo��$	��256��$	��256��$��50,0,0,0q�$
��Left=�
   Source$
 ../Resources/mybird.svg?�   OptimizePath .     TextAsGeometry . ]� .+��� $	��Top=� �� Orientation$ Vertical=�.+��� -    ��Name$ lbheader��$
��24pt(�$��Arial)����#4ABFB6$��Center=�$��0,35,0,0q�.��Login*   $MessangerClient.Controls.BirdTextBox  -     Name$ tbfirst��*   $MessangerClient.Controls.BirdTextBox   TextValueChanged$ tbfirst_TextValueChanged��$
��13pt(�$	��275��$��50��   DefaultText$ Email��$��0,33,0,0q�$��Center=��� -   $ lbfirst��$y�Hidden=�$
��Left=�$��83,5,0,0q����#FD878D$
��10pt(�$��Arial)�.��Incorrect email  -   $ tbsecond��$ tbsecond_TextValueChanged��$
��13pt(�$	��275��$��50��$ Password��   PasswordMode . $��0,20,0,0q��� -   $ lbsecond��$y�Hidden=�$
��Left=�$��83,5,0,0q����#FD878D$
��10pt(�$��Arial)�.��Incorrect password�� $��Bottom=�$ 
Horizontal=�.+��� -    ��Name$ btnfirst��   
birdbutton#��[  $��	60,0,40,0q����#4ABFB6
�������$��Arial)�$
��12pt(�$	��118��$��42��$��Submit���� -	   $ 	btnsecond��#��[  ���#4ABFB6
�������$��Arial)�$
��12pt(�$	��118��$��42��$��
Registrate��