package com.pb;

import javax.swing.JPanel;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.UIManager;
import javax.swing.UIManager.LookAndFeelInfo;

import java.awt.GridBagLayout;
import java.awt.GridBagConstraints;
import java.awt.Insets;
import javax.swing.JTextField;

public class ConfigurationPanel extends JPanel {
    private static final long            serialVersionUID = 700829005916276441L;
    private JComboBox< LookAndFeelName > cbLookAndFeel;
    private JTextField tfApiKey;

    private class LookAndFeelName {
        private String name;
        private String className;

        public LookAndFeelName( String name, String className ) {
            this.name = name;
            this.className = className;
        }

        public String getClassName() {
            return className;
        }

        @Override
        public String toString() {
            return name;
        }
    }

    public ConfigurationPanel( Config config ) {
        GridBagLayout gridBagLayout = new GridBagLayout();
        gridBagLayout.columnWeights = new double[]{0.0, 0.0, 1.0};
        setLayout( gridBagLayout );

        JLabel lLookAndFeel = new JLabel( "Look and feel" );
        GridBagConstraints gbcLLookAndFeel = new GridBagConstraints();
        gbcLLookAndFeel.anchor = GridBagConstraints.NORTHWEST;
        gbcLLookAndFeel.insets = new Insets( 5, 5, 5, 5 );
        gbcLLookAndFeel.gridx = 1;
        gbcLLookAndFeel.gridy = 0;
        add( lLookAndFeel, gbcLLookAndFeel );

        cbLookAndFeel = new JComboBox<>();
        GridBagConstraints gbcCbLookAndFeel = new GridBagConstraints();
        gbcCbLookAndFeel.insets = new Insets( 0, 0, 5, 0 );
        gbcCbLookAndFeel.fill = GridBagConstraints.HORIZONTAL;
        gbcCbLookAndFeel.gridx = 2;
        gbcCbLookAndFeel.gridy = 0;
        add( cbLookAndFeel, gbcCbLookAndFeel );

        JLabel lApiKey = new JLabel( "Steam API key" );
        GridBagConstraints gbcLApiKey = new GridBagConstraints();
        gbcLApiKey.anchor = GridBagConstraints.EAST;
        gbcLApiKey.insets = new Insets(5, 5, 0, 5);
        gbcLApiKey.gridx = 1;
        gbcLApiKey.gridy = 1;
        add( lApiKey, gbcLApiKey );

        for ( LookAndFeelInfo laf : UIManager.getInstalledLookAndFeels() ) {
            cbLookAndFeel.addItem( new LookAndFeelName( laf.getName(), laf.getClassName() ) );
            if ( config.getLookAndFeel().equals( laf.getClassName() ) ) {
                cbLookAndFeel.setSelectedIndex( cbLookAndFeel.getItemCount() - 1 );
            }
        }
        cbLookAndFeel.setSelectedItem( config.getLookAndFeel() );
        
        tfApiKey = new JTextField();
        tfApiKey.setText( config.getApiKey() );
        GridBagConstraints gbcTfApiKey = new GridBagConstraints();
        gbcTfApiKey.fill = GridBagConstraints.HORIZONTAL;
        gbcTfApiKey.gridx = 2;
        gbcTfApiKey.gridy = 1;
        add(tfApiKey, gbcTfApiKey);
    }

    public void applyConfig( Config config ) {
        config.setLookAndFeel( ( (LookAndFeelName)cbLookAndFeel.getSelectedItem() ).getClassName() );
        config.setApiKey( tfApiKey.getText() );
    }
}
