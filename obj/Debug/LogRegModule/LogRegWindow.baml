   M S B A M L   `   `   `  ����J  FMessangerClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MessangerClient.LogRegModuleMessangerClient.LogRegModule  5   	   C&clr-namespace:MessangerClient.ControlsMessangerClient.Controls  5   	   /    )MessangerClient.LogRegModule.LogRegWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:MessangerClient.LogRegModule     5   	   ;controls&clr-namespace:MessangerClient.Controls     5   	   b ^SharpVectors.Converters.Wpf, Version=1.6.0.0, Culture=neutral, PublicKeyToken=b532964b8548be771svgs&http://sharpvectors.codeplex.com/svgc/  5   	     � Title$  LogRegWindow��5
   	   $	��420��6   $	��700��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$
 None=�6\    � 
ResizeMode$ NoResize=�6o    � 	Resources 5       �� Source�� 5   
   $" ../Controls/Dictionary1.xaml?�6   %    6E   .��5      ]� ���#F2F2F2.+�5   
   %   MessangerClient.Controls.TopBar  -   5          Name$ topbar��5         TopBarHeader$ 
BirdSender��6*      
ResizeMode .  $	��Top=�6W   $	��Top=�6l   6�   (  "SharpVectors.Converters.SvgViewbox  5   
   -   5        	  Name$
	 logo��5      $	��256��6(   $	��256��64   $��50,0,0,0q�6A   $
��Left=�6S   
   Source$
 ../Resources/mybird.svg?�6o      OptimizePath .     TextAsGeometry . 6�   ]� 5   
   .+�5      �� $	��Top=�6    �� Orientation$ Vertical=�6.   .+�5      �� -   5         ��Name$ lbheader��5      $
��24pt(�6*   $��Arial)�6:   ���#4ABFB6$��Center=�6b   $��0,35,0,0q�6   .��6�   Login6�   *   $MessangerClient.Controls.BirdTextBox  5      -   5          Name$ tbfirst��5   7   *   $MessangerClient.Controls.BirdTextBox   TextValueChanged$ tbfirst_TextValueChanged��6�   $
��13pt(�6'   $	��275��6H   $��50��6T      DefaultText$ Email��6`   $��0,33,0,0q�6t   $��Center=�6�   6�   �� 5      -   5        $ lbfirst��5      $y�Hidden=�6)   $
��Left=�6=   $��83,5,0,0q�6X   ���#FD878D$
��10pt(�6   $��Arial)�6�   .��6�   Incorrect email6�     5      -   5        $ tbsecond��5   7   $ tbsecond_TextValueChanged��6�   $
��13pt(�6'   $	��275��6I   $��50��6U   $ Password��6a      PasswordMode . $��0,20,0,0q�6�   6�   �� 5      -   5        $ lbsecond��5      $y�Hidden=�6*   $
��Left=�6>   $��83,5,0,0q�6Y   ���#FD878D$
��10pt(�6�   $��Arial)�6�   .��6�   Incorrect password6�   5      �� 5      $��Bottom=�6   $ 
Horizontal=�61   .+�5      �� -   5         ��Name$ btnfirst��5         
birdbutton#��[  6+   $��	60,0,40,0q�6R   ���#4ABFB6
�������$��Arial)�6�   $
��12pt(�6�   $	��118��6�   $��42��6�   $��Submit��6�   6�   �� 5      -	   5        $ 	btnsecond��5      #��[  6,   ���#4ABFB6
�������$��Arial)�6|   $
��12pt(�6�   $	��118��6�   $��42��6�   $��
Registrate��6�   6�   5      5      5       5!      