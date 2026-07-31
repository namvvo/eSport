export const initDatePicker = (defaultDate,dateRange, isMonth) => {
    
    if (isMonth) {
        
        var fp = flatpickr("#week-picker", {  
            dateFormat: "m-Y",
            minDate: dateRange[0], // Set the minimum allowed date
            maxDate: dateRange[1], // Set the maximum allowed date           
            locale: "vn",
             onChange: function (selectedDates, dateStr, instance) {
                
                const selectedDate = selectedDates[0];
                const firstDay = new Date(selectedDate);
                fp.setDate(firstDay.getMonth() + 1 + '-' + firstDay.getFullYear());

                instance.close();//exit window

            },
            plugins: [
                new monthSelectPlugin({
                    shorthand: true, //defaults to false
                    dateFormat: "m-Y",
                    theme: "Default" // defaults to "light"
                })
            ]

        });
    }
    else {
        var fp = flatpickr("#week-picker", {
            dateFormat: "d-m-Y",
            mode: "range",
            defaultDate: defaultDate,            
            weekNumbers: true,
            theme: "dark",
            minDate: dateRange[0], // Set the minimum allowed date
            maxDate: dateRange[1], // Set the maximum allowed date     
            locale: "vn",
            onChange: function (selectedDates, dateStr, instance) {

                if (selectedDates.length > 0) {

                    const selectedDate = selectedDates[0];
                    const firstDay = new Date(selectedDate);
                    const firstDayOfWeek = new Date(selectedDate);
                    const lastDayOfWeek = new Date(selectedDate);
                    // Calculate the first day of the week (Sunday)
                    firstDayOfWeek.setDate(firstDay.getDate() - firstDay.getDay() + 1);
                    lastDayOfWeek.setDate(firstDay.getDate() - firstDay.getDay() + 7);
                    fp.setDate([firstDayOfWeek, lastDayOfWeek]);

                    instance.close();
                }
            }
        });
    }
}