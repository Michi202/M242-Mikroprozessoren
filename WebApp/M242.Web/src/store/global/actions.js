import moment from 'moment'

export const changeLocale = (context, locale) => {
    moment.locale(locale)
    var localeData = moment.localeData();
    var dateFormat = localeData.longDateFormat('L')

    context.commit("setLocale", locale);
    context.commit("setDateFormat", dateFormat);
};
export const changeProfileImageCacheDate = (context) => {
    context.commit("setProfileImageCachDate", moment().format());
};
