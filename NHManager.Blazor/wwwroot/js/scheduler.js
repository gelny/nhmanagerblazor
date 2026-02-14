window.schedulerInterop = {
    _calendar: null,
    _dotNetRef: null,
    _tooltip: null,

    initialize: function (elementId, dotNetRef, options) {
        this._dotNetRef = dotNetRef;
        var self = this;

        var calendarEl = document.getElementById(elementId);
        if (!calendarEl) return;

        this._calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: options.initialView || 'timeGridWeek',
            locale: options.locale || 'cs',
            headerToolbar: false,
            slotDuration: '00:15:00',
            slotLabelInterval: '01:00:00',
            slotMinTime: '06:00:00',
            slotMaxTime: '21:00:00',
            editable: true,
            selectable: false,
            nowIndicator: true,
            allDaySlot: false,
            height: 'auto',
            expandRows: true,
            dayMaxEvents: false,
            slotLabelFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            eventTimeFormat: {
                hour: '2-digit',
                minute: '2-digit',
                hour12: false
            },
            firstDay: 1,
            events: [],

            dateClick: function (info) {
                self._hideTooltip();
                dotNetRef.invokeMethodAsync('OnDateClick', info.dateStr, info.allDay);
            },

            eventClick: function (info) {
                self._hideTooltip();
                dotNetRef.invokeMethodAsync('OnEventClick', parseInt(info.event.id));
            },

            eventDrop: function (info) {
                self._hideTooltip();
                dotNetRef.invokeMethodAsync('OnEventDrop',
                    parseInt(info.event.id),
                    info.event.start.toISOString(),
                    info.event.end ? info.event.end.toISOString() : info.event.start.toISOString()
                );
            },

            eventResize: function (info) {
                self._hideTooltip();
                dotNetRef.invokeMethodAsync('OnEventResize',
                    parseInt(info.event.id),
                    info.event.start.toISOString(),
                    info.event.end.toISOString()
                );
            },

            eventDidMount: function (info) {
                var tooltipHtml = info.event.extendedProps.tooltipHtml;
                if (!tooltipHtml) return;

                info.el.addEventListener('mouseenter', function (e) {
                    self._showTooltip(e, tooltipHtml);
                });
                info.el.addEventListener('mouseleave', function () {
                    self._hideTooltip();
                });
                info.el.addEventListener('mousemove', function (e) {
                    self._moveTooltip(e);
                });
            },

            datesSet: function (info) {
                dotNetRef.invokeMethodAsync('OnDatesSet',
                    info.start.toISOString(),
                    info.end.toISOString(),
                    info.view.type
                );
            }
        });

        this._calendar.render();
    },

    changeView: function (viewName) {
        if (this._calendar) {
            this._calendar.changeView(viewName);
        }
    },

    gotoDate: function (dateStr) {
        if (this._calendar) {
            this._calendar.gotoDate(dateStr);
        }
    },

    today: function () {
        if (this._calendar) {
            this._calendar.today();
        }
    },

    prev: function () {
        if (this._calendar) {
            this._calendar.prev();
        }
    },

    next: function () {
        if (this._calendar) {
            this._calendar.next();
        }
    },

    getDate: function () {
        if (this._calendar) {
            return this._calendar.getDate().toISOString();
        }
        return new Date().toISOString();
    },

    getView: function () {
        if (this._calendar) {
            return this._calendar.view.type;
        }
        return 'timeGridWeek';
    },

    setEvents: function (events) {
        if (this._calendar) {
            this._calendar.removeAllEvents();
            for (var i = 0; i < events.length; i++) {
                this._calendar.addEvent(events[i]);
            }
        }
    },

    destroy: function () {
        this._hideTooltip();
        if (this._calendar) {
            this._calendar.destroy();
            this._calendar = null;
        }
        this._dotNetRef = null;
    },

    _showTooltip: function (e, html) {
        this._hideTooltip();
        var tooltip = document.createElement('div');
        tooltip.className = 'scheduler-tooltip';
        tooltip.innerHTML = html;
        document.body.appendChild(tooltip);
        this._tooltip = tooltip;
        this._positionTooltip(e);
    },

    _moveTooltip: function (e) {
        if (this._tooltip) {
            this._positionTooltip(e);
        }
    },

    _positionTooltip: function (e) {
        if (!this._tooltip) return;
        var x = e.clientX + 15;
        var y = e.clientY + 15;
        var rect = this._tooltip.getBoundingClientRect();
        if (x + rect.width > window.innerWidth) {
            x = e.clientX - rect.width - 15;
        }
        if (y + rect.height > window.innerHeight) {
            y = e.clientY - rect.height - 15;
        }
        this._tooltip.style.left = x + 'px';
        this._tooltip.style.top = y + 'px';
    },

    _hideTooltip: function () {
        if (this._tooltip) {
            this._tooltip.remove();
            this._tooltip = null;
        }
    }
};
