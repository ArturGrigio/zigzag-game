<template>
    <span>
        <span id="clock">
            <div class="block">
                <p class="digit" v-text="days_val"></p>
                <h1 class="text thin orange-text"><small>DAYS</small></h1>
            </div>
            <div class="block">
                <p class="digit" v-text="hours_val"></p>
                <h1 class="text thin orange-text"><small>HRS</small></h1>
            </div>
            <div class="block">
                <p class="digit" v-text="minutes_val"></p>
                <h1 class="text thin orange-text"><small>MINS</small></h1>
            </div>
            <div class="block">
                <p class="digit" v-text="seconds_val"></p>
                <h1 class="text thin orange-text"><small>SECS</small></h1>
            </div>
        </span>
        <div class="row no-margin center">
            <div class="btn orange" v-on:click="hideClock">
                <i class="mdi small white-text" v-bind:class="status ? 'mdi-chevron-down' : 'mdi-chevron-up'"></i>
            </div>
        </div>
    </span>
</template>

<script>
import moment from 'moment';

export default {
    props : {
        date : {
            type: String,
        }
    },

    data: () => {
        return {
            days_val: null,
            hours_val: null,
            minutes_val: null,
            seconds_val: null,
            status: false,
        }
    },

    methods: {
        clockTick() {
            let self = this;
            setInterval(() => {
                self.seconds()
                self.minutes()
                self.hours()
                self.days()
            }, 100);
        },
        seconds() {
            this.seconds_val = Math.abs(moment().diff(moment(this.date, 'MM-DD-YYYY'),'seconds')) % 60
        },
        minutes() {
            this.minutes_val = Math.abs(moment().diff(moment(this.date, 'MM-DD-YYYY'),'minutes')) % 60
        },
        hours() {
            this.hours_val = Math.abs(moment().diff(moment(this.date, 'MM-DD-YYYY'),'hours')) % 24
        },
        days() {
            this.days_val = Math.abs(moment().diff(moment(this.date, 'MM-DD-YYYY'),'days'))
        },
        hideClock() {
            console.log("test")
            if(!this.status) {
                this.status = true
                $('#clock').slideUp()
            } else {
                this.status = false
                $('#clock').slideDown()
            }
        }
    },

    mounted() {
        this.clockTick()
    }

}
</script>
<style>
@import url(https://fonts.googleapis.com/css?family=Roboto+Condensed:400|Roboto:100);
.block {
    margin: 20px;
    display: inline-block;
    min-width: 185px;
}
.text {
    margin-top:0px;
    margin-bottom: 10px;
    text-align: center;
}
.digit {
    color: #ecf0f1;
    font-size: 150px;
    line-height: 100px;
    font-weight: 100;
    font-family: 'Roboto', serif;
    margin: 0px;
    text-align: center;
}
</style>
