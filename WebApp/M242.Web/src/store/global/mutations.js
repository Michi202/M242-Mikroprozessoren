import Vue from 'vue'
import moment from 'moment'

export const setLocale = (state, locale) => {
    state.locale = locale
}

export const setDateFormat = (state, dateFormat) => {
    state.dateFormat = dateFormat
}
export const setProfileImageCachDate = (state, profileimagecachdate) => {
    state.profileimagecachdate = profileimagecachdate
}
