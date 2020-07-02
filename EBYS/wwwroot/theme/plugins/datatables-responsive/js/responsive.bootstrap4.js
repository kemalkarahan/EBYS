/*! Bootstrap 4 integration for DataTables' Responsive
 * ©2016 SpryMedia Ltd - datatables.net/license
 */

(function( factory ){
	if ( typeof define === 'function' && define.amd ) {
		// AMD
		define( ['jquery', 'datatables.net-bs4', 'datatables.net-responsive'], function ( $ ) {
			return factory( $, window, document );
		} );
	}
	else if ( typeof exports === 'object' ) {
		// CommonJS
		module.exports = function (root, $) {
			if ( ! root ) {
				root = window;
			}

			if ( ! $ || ! $.fn.dataTable ) {
				$ = require('datatables.net-bs4')(root, $).$;
			}

			if ( ! $.fn.dataTable.Responsive ) {
				require('datatables.net-responsive')(root, $);
			}

			return factory( $, root, root.document );
		};
	}
	else {
		// Browser
		factory( jQuery, window, document );
	}
}(function( $, window, document, undefined ) {
'use strict';
var DataTable = $.fn.dataTable;


var _display = DataTable.Responsive.display;
var _original = _display.moBusiness;
var _moBusiness = $(
	'<div class="moBusiness fade dtr-bs-moBusiness" role="dialog">'+
		'<div class="moBusiness-dialog" role="document">'+
			'<div class="moBusiness-content">'+
				'<div class="moBusiness-header">'+
					'<button type="button" class="close" data-dismiss="moBusiness" aria-label="Close"><span aria-hidden="true">&times;</span></button>'+
				'</div>'+
				'<div class="moBusiness-body"/>'+
			'</div>'+
		'</div>'+
	'</div>'
);

_display.moBusiness = function ( options ) {
	return function ( row, update, render ) {
		if ( ! $.fn.moBusiness ) {
			_original( row, update, render );
		}
		else {
			if ( ! update ) {
				if ( options && options.header ) {
					var header = _moBusiness.find('div.moBusiness-header');
					var button = header.find('button').detach();
					
					header
						.empty()
						.append( '<h4 class="moBusiness-title">'+options.header( row )+'</h4>' )
						.append( button );
				}

				_moBusiness.find( 'div.moBusiness-body' )
					.empty()
					.append( render() );

				_moBusiness
					.appendTo( 'body' )
					.moBusiness();
			}
		}
	};
};


return DataTable.Responsive;
}));
