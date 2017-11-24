<template>
    <span>
        <div class="spacer-4"></div>
        <h2 class="thin orange-text">Creators</h2>
        <div class="row">
            <div class="col l3 m6 s12" v-for="creatorObj in creators">
                <creator :creator="creatorObj"></creator>
            </div>
        </div>
        <div class="spacer-2"></div>
    </span>
</template>

<script>
    import creator from "./creator/Creator.vue";
    export default {
        components: { creator },
        data: ()=> {
            return {
                creators: [
                    {
                        name: "Artur Grigoryan",
                        image:"artur.jpg",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/ArturGrigio"
                            },
                            {
                                text: "StackOverflow",
                                url: "https://stackoverflow.com/users/2321515/grigio?tab=profile"
                            }
                        ]
                    },
                    {
                        name: "Michael Callan",
                        image:"michael.png",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/keiki83"
                            },
                        ]
                    },
                    {
                        name: "Huy Ngo",
                        image:"huy.png",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/pandabytes"
                            },
                        ]
                    },
                    {
                        name: "Matt Legaspi",
                        image:"matt.png",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/Zakkusu41"
                            },
                        ]
                    },
                    {
                        name: "Thomas Halstead",
                        image:"thomas.png",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/tomhalstead"
                            },
                        ]
                    },
                    {
                        name: "Daniel Ackerman",
                        image:"dan.png",
                        links: [
                            {
                                text: "GitHub",
                                url: "https://github.com/ackd151"
                            },
                        ]
                    },
                ]
            }
        }
    }
</script>
