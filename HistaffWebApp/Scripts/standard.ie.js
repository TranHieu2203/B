/**
 * Template JS for Internet Explorer 8 and lower - mainly workaround for missing selectors
 */

(function($) {

    
	$(document).ready(function()
	{
		// Missing selectors
		$(this).find(':first-child').addClass('first-child');
		$(this).find(':last-child').addClass('last-child');
		
		// Specific classes
		$(this).find('.head').each(function () { $(this).children('div:last').addClass('last-of-type'); });
		$(this).find('tbody tr:even, .task-dialog > li:even, .planning > li.planning-header > ul > li:even').addClass('even');
		$(this).find('tbody tr:odd, .planning > li:odd').addClass('odd');
		$(this).find('.form fieldset:has(legend)').addClass('fieldset-with-legend').filter(':first-child').addClass('fieldset-with-legend-first-child');
		
		// Disabled buttons
		$(this).find('button:disabled').addClass('disabled');
		
		// IE 7
		if ($.browser.version < 8)
		{
		    $(this).find('.block-content h1:first-child, .block-content .h1:first-child').next().addClass('after-h1');
		    $(this).find('.calendar .add-event').prepend('<span class="before"></span>');
		}
	});

})(jQuery);
